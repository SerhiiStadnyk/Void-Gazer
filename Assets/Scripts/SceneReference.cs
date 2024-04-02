using EditorScripts;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneRef_", menuName = "Game/Scene Reference", order = 1)]
public class SceneReference : ScriptableObject
{
    //TODO: Add Scene groups
    [SerializeField]
    [ReadOnly]
    private string _sceneId;

    public string SceneId => _sceneId;
}