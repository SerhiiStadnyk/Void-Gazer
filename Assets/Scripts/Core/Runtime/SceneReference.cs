using UnityEngine;
using Core.Inspector;

namespace Core.Runtime
{
    [CreateAssetMenu(fileName = "SceneRef_", menuName = "Game/Scene Reference", order = 1)]
    public class SceneReference : ScriptableObject
    {
        //TODO: Add Scene groups
        [SerializeField]
        [ReadOnly]
        private string _scenePath;

        public string ScenePath => _scenePath;
    }
}