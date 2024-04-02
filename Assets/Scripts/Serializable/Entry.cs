using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Serializable
{
    [Serializable]
    public class Entry
    {
        [SerializeField]
        private string _entryId;

        [SerializeField]
        private SerializableDictionary<string, string> _objects = new SerializableDictionary<string, string>();

        public SerializableDictionary<string, string> Objects => _objects;

        public string EntryId => _entryId;


        public Entry(string entryId)
        {
            _entryId = entryId;
        }


        public void Serialize()
        {
            _objects.Serialize();
        }


        public void Deserialize()
        {
            _objects.Deserialize();
        }


        public void SetObject(string fieldId, object value)
        {
            string json = JsonUtility.ToJson(value);
            if (string.IsNullOrEmpty(json) || json == "{}")
            {
                json = "{" + value + "}";
            }
            _objects.AddOrUpdate(fieldId, json);
        }


        public T GetObject<T>(string fieldId)
        {
            T result = default;
            if (_objects.ContainsKey(fieldId))
            {
                result = JsonUtility.FromJson<T>(_objects[fieldId]);
            }
            return result;
        }


        public List<T> GetObjectList<T>(string fieldId)
        {
            List<T> result = new List<T>();
            if (_objects.ContainsKey(fieldId))
            {
                SerializableList<T> serializableList = JsonUtility.FromJson<SerializableList<T>>(_objects[fieldId]);
                result = serializableList.values;
            }

            return result;
        }


        public void SetObjectList<T>(string fieldId, List<T> list)
        {
            var serializableList = new SerializableList<T>(list);
            string json = JsonUtility.ToJson(serializableList);
            _objects.AddOrUpdate(fieldId, json);
        }


        public Dictionary<TKey, TValue> GetObjectDictionary<TKey, TValue>(string fieldId)
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
            if (_objects.ContainsKey(fieldId))
            {
                SerializableDictionary<TKey, TValue> serializableDictionary = JsonUtility.FromJson<SerializableDictionary<TKey, TValue>>(_objects[fieldId]);
                serializableDictionary.Deserialize();
                result = serializableDictionary.Dictionary;
            }

            return result;
        }


        public void SetObjectDictionary<TKey, TValue>(string fieldId, Dictionary<TKey, TValue> dictionary)
        {
            var serializableDictionary = new SerializableDictionary<TKey, TValue>(dictionary);
            serializableDictionary.Serialize();
            string json = JsonUtility.ToJson(serializableDictionary);
            _objects.AddOrUpdate(fieldId, json);
        }
    }


    [Serializable]
    public class SerializableList<T>
    {
        public List<T> values;

        public SerializableList(List<T> list)
        {
            values = list;
        }
    }
}
