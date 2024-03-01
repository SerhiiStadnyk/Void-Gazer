using System;
using System.Collections.Generic;
using UnityEngine;

namespace Serializable
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue>
    {
        [SerializeField]
        private List<TKey> _keys;

        [SerializeField]
        private List<TValue> _values;

        private Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();


        public SerializableDictionary(int count = 0)
        {
            _keys = new List<TKey>(count);
            _values = new List<TValue>(count);
        }


        // Indexer to access dictionary entries
        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }


        // Implement this method to add data to the dictionary during serialization
        public void OnBeforeSerialize()
        {
            _keys.Clear();
            _values.Clear();

            foreach (KeyValuePair<TKey, TValue> pair in _dictionary)
            {
                _keys.Add(pair.Key);
                _values.Add(pair.Value);
            }
        }


        // Implement this method to rebuild the dictionary from serialized data
        public void OnAfterDeserialize()
        {
            _dictionary.Clear();

            if (_keys.Count != _values.Count)
            {
                throw new Exception($"The number of keys ({_keys.Count}) and values ({_values.Count}) does not match.");
            }

            for (int i = 0; i < _keys.Count; i++)
            {
                _dictionary[_keys[i]] = _values[i];
            }
        }


        // Method to add or update a dictionary entry
        public void AddOrUpdate(TKey key, TValue value)
        {
            _dictionary[key] = value;
        }


        // Method to remove a dictionary entry
        public bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }


        // Method to check if the dictionary contains a key
        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }


        // Method to clear all entries from the dictionary
        public void Clear()
        {
            _dictionary.Clear();
        }
    }
}