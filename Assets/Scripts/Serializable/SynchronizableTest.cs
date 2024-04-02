using System;
using System.Collections.Generic;
using EditorScripts;
using Synchronizable;
using UnityEngine;

namespace Serializable
{
    public class SynchronizableTest : MonoBehaviour, ISynchronizable
    {
        [SerializeField]
        private List<string> _testList;

        [SerializeField]
        private List<TestDictPair> _testDictPairs;

        [SerializeField]
        [ReadOnly]
        private string _id;


        string IIdHolder.Id
        {
            get => _id;
            set => _id = value;
        }


        void ISynchronizable.SaveData(Entry entry)
        {
            entry.SetObjectList(nameof(_testList), _testList);

            Dictionary<string, int> dictionary = new Dictionary<string, int>(_testDictPairs.Count);
            foreach (TestDictPair pair in _testDictPairs)
            {
                dictionary.Add(pair._key, pair._value);
            }
            entry.SetObjectDictionary(nameof(_testDictPairs), dictionary);
        }


        void ISynchronizable.LoadData(Entry entry)
        {
            _testList = entry.GetObjectList<string>(nameof(_testList));

            Dictionary<string, int> dictionary = entry.GetObjectDictionary<string, int>(nameof(_testDictPairs));
            _testDictPairs = new List<TestDictPair>();
            foreach (KeyValuePair<string, int> pair in dictionary)
            {
                _testDictPairs.Add(new TestDictPair(pair.Key, pair.Value));
            }
        }


        [Serializable]
        private class TestDictPair
        {
            public string _key;
            public int _value;


            public TestDictPair(string key, int value)
            {
                _key = key;
                _value = value;
            }
        }
    }
}
