using Core.Runtime;
using UnityEditor;

namespace Core.Inspector
{
    [CustomEditor(typeof(SceneReference))]
    public class SceneReferenceEditor : UnityEditor.Editor
    {
        private SerializedProperty _sceneIdProperty;
        private SceneAsset _sceneAsset;


        public void OnEnable()
        {
            _sceneIdProperty = serializedObject.FindProperty("_sceneId");
            serializedObject.Update();

            if (_sceneIdProperty.stringValue != null)
            {
                _sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(AssetDatabase.GUIDToAssetPath(_sceneIdProperty.stringValue));
            }
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            SceneAsset newSceneAsset = EditorGUILayout.ObjectField(_sceneAsset, typeof(SceneAsset), true) as SceneAsset;

            if (_sceneAsset != newSceneAsset)
            {
                _sceneAsset = newSceneAsset;
                _sceneIdProperty.stringValue = _sceneAsset == null ? null : AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(_sceneAsset)).ToString();

                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
