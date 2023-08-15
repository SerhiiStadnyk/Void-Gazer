using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Editor
{
    [CustomPropertyDrawer(typeof(DrawableTypeReference), true)]
    public class TypeReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Type valueType = GetTypeOfSerializedProperty(property);
            Type refType = valueType.GetGenericArguments()[0];

            SerializedProperty typeStringProperty = property.FindPropertyRelative("_serializableType");

            bool Filter(Type t) => t.BaseType == refType;

            Type[] types = refType.Assembly.GetTypes().Where(Filter).ToArray();

            int selectedIndex = 0;
            for (int i = 0; i < types.Length; ++i)
            {
                string serializedString = InvokeMethod(property, refType, types[i]);
                if (serializedString == typeStringProperty.stringValue)
                {
                    selectedIndex = i + 1;
                    break;
                }
            }

            string[] names =
            {
                "None"
            };
            names = names.Concat(from t in types select t.FullName.Substring(t.Namespace.Length + 1).Replace('+', '.'))
                .ToArray();

            int newIndex = EditorGUI.Popup(
                position,
                selectedIndex,
                names
            );
            if (newIndex != selectedIndex)
            {
                string serializedString = string.Empty;
                if (newIndex > 0)
                {
                    serializedString = InvokeMethod(property, refType, types[newIndex - 1]);
                }

                typeStringProperty.stringValue = serializedString;
            }

            property.serializedObject.ApplyModifiedProperties();
        }


        private string InvokeMethod(SerializedProperty property, Type genericType, Type parameterType)
        {
            object returnValue = null;
            if (parameterType != null)
            {
                MethodInfo serializeMethod = typeof(TypeReference<>)
                    .MakeGenericType(genericType)
                    .GetMethod("Serialize");

                object referenceObject = GetPropertyInstance(property);
                returnValue = serializeMethod.Invoke(referenceObject, new object[]
                {
                    parameterType
                });
            }

            return returnValue as string;
        }


        private object GetNestedPropertyParent(SerializedProperty property)
        {
            Type targetType = property.serializedObject.targetObject.GetType();
            string[] propertyPathSegments = property.propertyPath.Split('.');

            FieldInfo nestedField = targetType.GetField(propertyPathSegments[0], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            object result = nestedField.GetValue(property.serializedObject.targetObject);

            return result;
        }


        private object GetArrayPropertyParent(SerializedProperty property)
        {
            Type targetType = property.serializedObject.targetObject.GetType();
            string[] propertyPathSegments = property.propertyPath.Split('.');

            Type currentRootType = targetType;
            object currentRoot = null;
            for (int i = 0; i < propertyPathSegments.Length; i++)
            {
                if (propertyPathSegments[i] == "Array")
                {
                    FieldInfo nestedField = currentRootType.GetField(propertyPathSegments[i - 1], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    int index = GetArrayElementIndexFromPropertyPath(propertyPathSegments);
                    currentRoot = ((IList)nestedField.GetValue(property.serializedObject.targetObject))[index];
                    i++;
                }
            }

            return currentRoot;
        }


        private int GetArrayElementIndexFromPropertyPath(string[] propertyPathSegments)
        {
            int arraySegmentIndex = 0;
            for (int i = 0; i < propertyPathSegments.Length; i++)
            {
                string segment = propertyPathSegments[i];
                if (segment == "Array")
                {
                    arraySegmentIndex = ++i;
                    break;
                }
            }

            int startIndex = propertyPathSegments[arraySegmentIndex].IndexOf("[", StringComparison.Ordinal) + 1;
            int endIndex = propertyPathSegments[arraySegmentIndex].IndexOf("]", StringComparison.Ordinal);
            string numberString = propertyPathSegments[2].Substring(startIndex, endIndex - startIndex);
            int index = int.Parse(numberString);

            return index;
        }


        private object GetPropertyParent(SerializedProperty property)
        {
            object result = property.serializedObject.targetObject;

            string[] propertyPathSegments = property.propertyPath.Split('.');
            switch (propertyPathSegments.Length)
            {
                case 2:
                    result = GetNestedPropertyParent(property);
                    break;
                case > 2:
                    result = GetArrayPropertyParent(property);
                    break;
            }

            return result;
        }


        private object GetPropertyInstance(SerializedProperty property)
        {
            string[] propertyPathSegments = property.propertyPath.Split('.');
            object parentObject = GetPropertyParent(property);
            FieldInfo propertyField = parentObject.GetType().GetField(propertyPathSegments[^1], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return propertyField.GetValue(parentObject);
        }


        private FieldInfo GetFieldInfo(SerializedProperty property)
        {
            string[] propertyPathSegments = property.propertyPath.Split('.');
            Type fieldParentTpe = GetPropertyParent(property).GetType();
            return fieldParentTpe.GetField(propertyPathSegments[^1], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }


        private Type GetTypeOfSerializedProperty(SerializedProperty property)
        {
            return GetFieldInfo(property).FieldType;
        }
    }
}
