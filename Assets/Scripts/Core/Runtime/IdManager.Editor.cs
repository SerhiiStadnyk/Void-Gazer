#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Core.Runtime
{
    public partial class IdManager
    {
        [SerializeField]
        private string _scenesLabel = "Scenes";

        private static IdManager _instance;
        private const string Scene_Folder_Path = "Assets/Data/Game/Runtime/Scenes";
        private const string STATIC_ID_PREFIX = "Static:";

        public static IdManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<IdManager>("Core/Editor");

                    if (_instance == null)
                    {
                        // If the asset doesn't exist, create a new instance
                        _instance = CreateAsset();
                    }
                }

                return _instance;
            }
        }


        [MenuItem("Game/Register Static Ids")]
        public static async void GenerateStaticIds()
        {
            Instance._staticGuids.Clear();

            List<string> scenePaths = await GetScenesPath();
            foreach (string scenePath in scenePaths)
            {
                Scene scene = EditorSceneManager.OpenScene(scenePath);
                GameObject rootGameObject = SceneManager.GetActiveScene().GetRootGameObjects()[0];
                ProcessGameObjectAndChildren(rootGameObject);
                EditorSceneManager.SaveScene(scene);
            }
        }


        private static void ProcessGameObjectAndChildren(GameObject gameObject)
        {
            ProcessGameObject(gameObject);
            foreach (Transform child in gameObject.transform)
            {
                ProcessGameObjectAndChildren(child.gameObject);
            }
        }

        private static void ProcessGameObject(GameObject gameObject)
        {
            Component[] components = gameObject.GetComponents<Component>();

            foreach (var component in components)
            {
                if (component is IInstanceIdHolder idHolder)
                {
                    if (string.IsNullOrEmpty(idHolder.InstanceId))
                    {
                        string guid = $"{STATIC_ID_PREFIX}{Instance.GenerateStaticGuid().ToString()}";
                        idHolder.InstanceId = guid;
                        Instance._staticGuids.Add(guid);
                        EditorUtility.SetDirty(idHolder as Component);
                    }
                    else
                    {
                        Instance._staticGuids.Add(idHolder.InstanceId);
                    }
                }
            }
        }


        private Guid GenerateStaticGuid()
        {
            Guid result = Guid.NewGuid();
            while (_staticGuids.Contains(result.ToString()))
            {
                result = Guid.NewGuid();
            }

            return result;
        }


        private static IdManager CreateAsset()
        {
            CreateFolders("Resources", "Core", "Editor");

            IdManager newInstance = CreateInstance<IdManager>();
            string assetPath = "Assets/Resources/Core/Editor/IdManager.asset";
            AssetDatabase.CreateAsset(newInstance, assetPath);
            AssetDatabase.SaveAssets();

            return newInstance;
        }


        private static void CreateFolders(params string[] folders)
        {
            string currentPath = "Assets/";
            foreach (string folderName in folders)
            {
                string newPath = $"{currentPath}/{folderName}";
                if (!AssetDatabase.IsValidFolder(newPath))
                {
                    AssetDatabase.CreateFolder(currentPath, folderName);
                }

                currentPath = newPath;
            }
        }


        private static async Task<List<string>> GetScenesPath()
        {
            List<string> result = new List<string>();

            AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync(Instance._scenesLabel);
            Task<IList<IResourceLocation>> task = handle.Task;
            IList<IResourceLocation> locations = await task;

            foreach (IResourceLocation location in locations)
            {
                if (location.ResourceType == typeof(SceneInstance))
                {
                    result.Add(location.InternalId);
                }
            }

            return result;
        }
    }
}

#endif