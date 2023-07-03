using UnityEditor;
using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.Scene
{
    [CreateAssetMenu(fileName = "SceneRef_.asset", menuName = "Mutatronic/Runtime/Core/Scene Reference")]
    public class SceneReference : ScriptableObject
    {
        [SerializeField]
        private SceneAsset _sceneAsset;

        public string ScenePath => AssetDatabase.GetAssetPath(_sceneAsset);

        public GUID SceneGUID => AssetDatabase.GUIDFromAssetPath(ScenePath);

        public SceneAsset SceneAsset => _sceneAsset;
    }
}