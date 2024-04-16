using Core.Runtime;
using UnityEditor;

namespace Core.Inspector
{
    [CustomEditor(typeof(SceneReference))]
    public class SceneReferenceEditor : Editor
    {
        private SerializedProperty _scenePathProperty;


        public void OnEnable()
        {
            _scenePathProperty = serializedObject.FindProperty("_scenePath");
            serializedObject.Update();
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            SceneAsset sceneAsset = null;
            if (!string.IsNullOrEmpty(_scenePathProperty.stringValue))
            {
                sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(_scenePathProperty.stringValue);
            }

            EditorGUI.BeginChangeCheck();
            sceneAsset = EditorGUILayout.ObjectField("Scene Asset", sceneAsset, typeof(SceneAsset), true) as SceneAsset;

            if (EditorGUI.EndChangeCheck())
            {
                _scenePathProperty.stringValue = sceneAsset == null ? null : AssetDatabase.GetAssetPath(sceneAsset);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
