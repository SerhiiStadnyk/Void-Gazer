using System.Collections.Generic;
using System.IO;
using System.Linq;
using EditorScripts;
using Serializable;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "IdManager", menuName = "Game/Core/IdManager", order = 1)]
public class IdManager : ScriptableObject, ISerializationCallbackReceiver, ISaveable
{
    [SerializeField]
    [ReadOnly]
    private List<string> _staticGuidsList = new List<string>();

    [SerializeField]
    [ReadOnly]
    private List<string> _dynamicGuidsList = new List<string>();

    private string _id = nameof(IdManager);

    private HashSet<string> _staticGuids = new HashSet<string>();
    private HashSet<string> _dynamicGuids = new HashSet<string>();

    private static IdManager _instance;

    private const string Scene_Folder_Path = "Assets/Data/Scenes";


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
    public static void GenerateStaticIds()
    {
        Instance._staticGuids.Clear();
        foreach (string scenePath in Instance.GetScenes())
        {
            Scene scene = EditorSceneManager.OpenScene(scenePath);

            IIdHolder[] idHolders = FindObjectsByType<Component>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .OfType<IIdHolder>()
                .ToArray();

            foreach (IIdHolder idHolder in idHolders)
            {
                if (string.IsNullOrEmpty(idHolder.Id))
                {
                    string guid = Instance.GenerateGuid().ToString();
                    idHolder.Id = guid;
                    Instance._staticGuids.Add(guid);
                    EditorUtility.SetDirty(idHolder as Component);
                }
                else
                {
                    Instance._staticGuids.Add(idHolder.Id);
                }
            }
            EditorSceneManager.SaveScene(scene);
        }
    }


    public GUID GenerateGuid()
    {
        GUID result = GUID.Generate();
        while (_staticGuids.Contains(result.ToString()) || _dynamicGuids.Contains(result.ToString()))
        {
            result = GUID.Generate();
        }

        return result;
    }


    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        _staticGuidsList.Clear();
        _staticGuidsList.AddRange(_staticGuids);
    }


    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        _staticGuids = new HashSet<string>(_staticGuidsList);
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


    private List<string> GetScenes()
    {
        var result = new List<string>();
        string[] sceneFiles = Directory.GetFiles(Scene_Folder_Path, "*.unity");
        foreach (string sceneFile in sceneFiles)
        {
            result.Add(sceneFile);
        }

        return result;
    }


    string IIdHolder.Id
    {
        get => _id;
        set => _id = value;
    }


    void ISaveable.SaveData(Entry entry)
    {
        _dynamicGuidsList.Clear();
        _dynamicGuidsList.AddRange(_dynamicGuids);

        entry.SetObject<string>(nameof(_dynamicGuidsList), _dynamicGuidsList);
    }


    void ISaveable.LoadData(Entry entry)
    {
        Debug.LogWarning("Load IdManager");
        _dynamicGuidsList = entry.GetObjectList<string>(nameof(_dynamicGuidsList));
        _dynamicGuids = new HashSet<string>(_dynamicGuidsList);
    }
}