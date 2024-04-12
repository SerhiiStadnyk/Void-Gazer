using System;
using System.Collections.Generic;
using System.Linq;
using Core.Runtime.Serializable;
using UnityEngine;
using Zenject;

namespace Core.Runtime.Synchronizable
{
    public class SceneSyncHandler : MonoBehaviour, ISynchronizable, IDisposable, ISceneObjectsInit
    {
        private SaveManager _saveManager;
        private SceneLifetimeHandler _sceneLifetimeHandler;

        private Dictionary<string, ISynchronizable> _saveables;
        private Dictionary<string, Entry> _sceneEntries;

        public event Action<Dictionary<string, Entry>> OnBeforeLoad;

        public Dictionary<string, Entry> SceneEntries => _sceneEntries;

        public IReadOnlyDictionary<string, ISynchronizable> Saveables => _saveables.ToDictionary(entry => entry.Key, entry => entry.Value);


        [Inject]
        public void Inject(SaveManager saveManager, SceneLifetimeHandler sceneLifetimeHandler)
        {
            _saveManager = saveManager;
            _sceneLifetimeHandler = sceneLifetimeHandler;
        }


        string IInstanceIdHolder.InstanceId
        {
            get => gameObject.scene.name;
            set
            {
            }
        }


        void IDisposable.Dispose()
        {
            _sceneLifetimeHandler.SceneClosing -= OnSceneClosing;
            if (_saveables != null && _saveables.Count > 0)
            {
                _saveManager.UnregisterSyncHandler(this);
                _saveables.Clear();
            }
        }


        void ISynchronizable.SaveData(Entry entry)
        {
            _sceneEntries = new Dictionary<string, Entry>();
            foreach (KeyValuePair<string, ISynchronizable> saveablePair in _saveables)
            {
                if (saveablePair.Key != gameObject.scene.name)
                {
                    Entry sceneEntry = new Entry(saveablePair.Key);
                    saveablePair.Value.SaveData(sceneEntry);
                    sceneEntry.Serialize();
                    if (_sceneEntries.ContainsKey(sceneEntry.EntryId))
                    {
                        _sceneEntries[sceneEntry.EntryId] = sceneEntry;
                    }
                    else
                    {
                        _sceneEntries.Add(sceneEntry.EntryId, sceneEntry);
                    }
                }
            }

            entry.SetObjectDictionary(nameof(_sceneEntries), _sceneEntries);
        }


        void ISynchronizable.LoadData(Entry entry)
        {
            _sceneEntries = entry.GetObjectDictionary<string, Entry>(nameof(_sceneEntries));
            OnBeforeLoad?.Invoke(_sceneEntries);
            LoadObjectsData(_sceneEntries);
        }


        void ISceneObjectsInit.InitObject(GameObject obj)
        {
            ISynchronizable[] saveables = obj.GetComponentsInChildren<ISynchronizable>(true);
            foreach (ISynchronizable saveable in saveables)
            {
                _saveables.Add(saveable.InstanceId, saveable);
            }
        }


        void ISceneObjectsInit.DisposeObject(GameObject obj)
        {
            ISynchronizable[] saveables = obj.GetComponentsInChildren<ISynchronizable>(true);
            foreach (ISynchronizable saveable in saveables)
            {
                _saveables.Remove(saveable.InstanceId);
            }
        }


        protected void Awake()
        {
            _sceneLifetimeHandler.SceneClosing += OnSceneClosing;
            _saveManager.RegisterSyncHandler(this);

            GetSynchronizables();
        }


        protected void Start()
        {
            //TODO: Fix this
            _saveManager.LoadSceneData(this);
        }


        private void OnSceneClosing(SceneLifetimeHandler.SceneClosingType closingType)
        {
            if (closingType == SceneLifetimeHandler.SceneClosingType.Unload)
            {
                _saveManager.SaveObjectData(this);
            }
        }


        //TODO: Make editor caching for synchronizables
        private void GetSynchronizables()
        {
            _saveables = new Dictionary<string, ISynchronizable>();

            Component[] components = GetComponentsInChildren<Component>(true);
            foreach (Component component in components)
            {
                if (component is ISynchronizable saveable && component != this)
                {
                    _saveables.Add(saveable.InstanceId, saveable);
                }
            }
        }


        private void LoadObjectsData(Dictionary<string, Entry> entries)
        {
            foreach (KeyValuePair<string, ISynchronizable> pair in _saveables)
            {
                if (entries.ContainsKey(pair.Key))
                {
                    Entry entry = entries[pair.Key];
                    entry.Deserialize();
                    pair.Value.LoadData(entry);
                }
                else
                {
                    Debug.LogWarning($"Garbage object in scene {gameObject.scene.name} with id: {pair.Key}");
                }
            }
        }
    }
}