using System;
using System.Collections.Generic;
using Core.Inspector;
using Core.Runtime.Serializable;
using Core.Runtime.Synchronizable;
using UnityEngine;

namespace Core.Runtime
{
    [CreateAssetMenu(fileName = "IdManager", menuName = "Game/Core/IdManager", order = 1)]
    public partial class IdManager : ScriptableObject, ISerializationCallbackReceiver, ISynchronizable
    {
        [SerializeField]
        [ReadOnly]
        private List<string> _staticGuidsList = new List<string>();

        [SerializeField]
        [ReadOnly]
        private List<string> _dynamicGuidsList = new List<string>();

        private HashSet<string> _staticGuids = new HashSet<string>();
        private HashSet<string> _dynamicGuids = new HashSet<string>();


        public Guid GenerateGuid()
        {
            Guid result = Guid.NewGuid();
            while (_staticGuids.Contains(result.ToString()) || _dynamicGuids.Contains(result.ToString()))
            {
                result = Guid.NewGuid();
            }

            _dynamicGuids.Add(result.ToString());
            return result;
        }


        public void RemoveDynamicGuid(string guid)
        {
            if (_dynamicGuids.Contains(guid))
            {
                _dynamicGuids.Remove(guid);
            }
        }


        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            _staticGuidsList.Clear();
            _staticGuidsList.AddRange(_staticGuids);
        }


        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            _staticGuids = new HashSet<string>(_staticGuidsList);
        }


        string IInstanceIdHolder.InstanceId
        {
            get => nameof(IdManager);
            set => Debug.LogError($"InstanceId of {nameof(IdManager)} should be immutable");
        }


        void ISynchronizable.SaveData(Entry entry)
        {
            _dynamicGuidsList.Clear();
            _dynamicGuidsList.AddRange(_dynamicGuids);

            entry.SetObject(nameof(_dynamicGuidsList), _dynamicGuidsList);
        }


        void ISynchronizable.LoadData(Entry entry)
        {
            _dynamicGuidsList = entry.GetObjectList<string>(nameof(_dynamicGuidsList));
            _dynamicGuids = new HashSet<string>(_dynamicGuidsList);
        }
    }
}