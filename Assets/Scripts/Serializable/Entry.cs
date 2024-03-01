using System;
using UnityEngine;

namespace Serializable
{
    [Serializable]
    public class Entry
    {
        [SerializeField]
        private string _entryId;

        [SerializeField]
        private SerializableDictionary<string, Vector3> _vector3Value = new SerializableDictionary<string, Vector3>();

        public string EntryId => _entryId;


        public Entry(string entryId)
        {
            _entryId = entryId;
        }


        public void Serialize()
        {
            _vector3Value.OnBeforeSerialize();
        }


        public void Deserialize()
        {
            _vector3Value.OnAfterDeserialize();
        }


        public void SetVector3(string fieldId, Vector3 value)
        {
            if (!_vector3Value.ContainsKey(fieldId))
            {
                _vector3Value.AddOrUpdate(fieldId, value);
            }
            else
            {
                Debug.LogError($"Entry already contains field with id {fieldId}");
            }
        }


        public Vector3 GetVector3(string fieldId)
        {
            Vector3 result = Vector3.zero;
            if (_vector3Value.ContainsKey(fieldId))
            {
                result = _vector3Value[fieldId];
            }
            return result;
        }
    }
}
