using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Serializable
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        [SerializeField]
        private List<TKey> _keys;

        [SerializeField]
        private List<TValue> _values;

        private Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

        public Dictionary<TKey, TValue> Dictionary => _dictionary;


        public SerializableDictionary(int count = 0)
        {
            _keys = new List<TKey>(count);
            _values = new List<TValue>(count);
        }


        public SerializableDictionary(Dictionary<TKey, TValue> dictionary)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary);
        }


        // Indexer to access dictionary entries
        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }


        // Implement this method to add data to the dictionary during serialization
        public void Serialize()
        {
            _keys ??= new List<TKey>();
            _values ??= new List<TValue>();
            _keys.Clear();
            _values.Clear();

            foreach (KeyValuePair<TKey, TValue> pair in _dictionary)
            {
                _keys.Add(pair.Key);
                _values.Add(pair.Value);
            }
        }


        // Implement this method to rebuild the dictionary from serialized data
        public void Deserialize()
        {
            if (_keys.Count != _values.Count)
            {
                throw new Exception($"The number of keys ({_keys.Count}) and values ({_values.Count}) does not match.");
            }

            _dictionary ??= new Dictionary<TKey, TValue>(_keys.Count);
            _dictionary.Clear();

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


        // Implementation of IEnumerable.GetEnumerator method
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }


        // Explicit implementation of IEnumerable.GetEnumerator method
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}