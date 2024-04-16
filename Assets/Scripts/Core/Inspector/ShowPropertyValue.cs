#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Core.Inspector
{
    public class ShowPropertyValueAttribute : PropertyAttribute { }

    // Custom property drawer for string fields with ShowStringValue attribute
    [CustomPropertyDrawer(typeof(ShowPropertyValueAttribute))]
    public class ShowStringValueDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Ensure that the field is a string
            if (property.propertyType == SerializedPropertyType.String)
            {
                // Get the string value
                string stringValue = property.stringValue;

                // Display the string value
                EditorGUI.LabelField(position, label.text, stringValue);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use ShowPropertyValueAttribute with string fields only");
            }
        }
    }
}

#endif