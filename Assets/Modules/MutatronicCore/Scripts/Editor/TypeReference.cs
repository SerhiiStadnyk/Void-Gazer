using System;
using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Editor
{
    public abstract class DrawableTypeReference
    {
    }


    [Serializable]
    public class TypeReference<T> : DrawableTypeReference, ISerializationCallbackReceiver where T: class
    {
        [SerializeField]
        private string _serializableType;

        private Type _type;

        public Type Type => _type;


        public TypeReference(Type type)
        {
            if (IsValidType(type))
            {
                _serializableType = Serialize(type);
            }
        }


        protected TypeReference()
        {
        }


        public string Serialize(Type type)
        {
            return type.AssemblyQualifiedName;
        }


        private bool IsValidType(Type type)
        {
            return !type.IsAbstract;
        }


        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }


        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            _type = Type.GetType(_serializableType);
        }
    }
}
