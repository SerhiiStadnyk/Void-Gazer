using System;
using System.Collections.Generic;
using System.Linq;
using Core.Runtime.Forms;
using UnityEngine;

namespace Core.Runtime
{
    [CreateAssetMenu(fileName = "ItemMap", menuName = "Game/Maps/ItemMap", order = 1)]
    public class ItemMap : ScriptableObject
    {
        [SerializeField]
        private List<KeyValuePair> _valuePairs;


        public GameObject GetPrefab(ItemForm itemForm)
        {
            return _valuePairs.FirstOrDefault(pair => pair.ItemForm == itemForm).Prefab;
        }


        [Serializable]
        private class KeyValuePair
        {
            [SerializeField]
            private ItemForm _itemForm;

            [SerializeField]
            private GameObject _prefab;

            public ItemForm ItemForm => _itemForm;

            public GameObject Prefab => _prefab;
        }
    }
}