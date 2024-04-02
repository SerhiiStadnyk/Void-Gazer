using System;
using System.Collections.Generic;
using System.Linq;
using Serializable;
using UnityEngine;
using Zenject;

namespace Synchronizable
{
    public class SceneSyncHandler : MonoBehaviour, ISynchronizable, IDisposable, ISceneObjectsInit
    {
        private SaveManager _saveManager;

        private Dictionary<string, ISynchronizable> _saveables;
        private Dictionary<string, Entry> _sceneEntries;

        public event Action<Dictionary<string, Entry>> OnBeforeLoad;

        public Dictionary<string, Entry> SceneEntries => _sceneEntries;

        public Dictionary<string, ISynchronizable> Saveables => _saveables;


        [Inject]
        public void Inject(SaveManager saveManager)
        {
            _saveManager = saveManager;
        }


        string IIdHolder.Id
        {
            get => gameObject.scene.name;
            set
            {
            }
        }


        void IDisposable.Dispose()
        {
            if (_saveables != null && _saveables.Count > 0)
            {
                _saveManager.SaveObjectData(this);
                _saveManager.UnregisterSyncHandler(this);
                _saveables.Clear();
            }
        }


        void ISynchronizable.SaveData(Entry entry)
        {
            _sceneEntries ??= new Dictionary<string, Entry>();
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


        public void AltSave(SaveManager saveManager)
        {
            foreach (KeyValuePair<string, ISynchronizable> saveablePair in _saveables)
            {
                if (saveablePair.Key != gameObject.scene.name)
                {
                    saveManager.SaveObjectData(saveablePair.Value);
                }
            }
        }


        public void AltLoad(SaveFile saveFile)
        {
            OnBeforeLoad?.Invoke(_sceneEntries);
            LoadObjectsData(saveFile.Entries.Dictionary);
        }


        void ISynchronizable.LoadData(Entry entry)
        {
            // _sceneEntries = entry.GetObjectDictionary<string, Entry>(nameof(_sceneEntries));
            // OnBeforeLoad?.Invoke(_sceneEntries);
            // LoadObjectsData(_sceneEntries);
        }


        void ISceneObjectsInit.InitObject(GameObject obj)
        {
            ISynchronizable[] saveables = obj.GetComponentsInChildren<ISynchronizable>(true);
            foreach (ISynchronizable saveable in saveables)
            {
                _saveables.Add(saveable.Id, saveable);
            }
        }


        void ISceneObjectsInit.DisposeObject(GameObject obj)
        {
            ISynchronizable[] saveables = obj.GetComponentsInChildren<ISynchronizable>(true);
            foreach (ISynchronizable saveable in saveables)
            {
                _saveables.Remove(saveable.Id);
            }
        }


        protected void Awake()
        {
            GetSynchronizables();
            _saveManager.RegisterSyncHandler(this);

            //TODO: Fix this
            _saveManager.LoadSceneData(this);
        }


        //TODO: Make editor caching for synchronizables
        private void GetSynchronizables()
        {
            _saveables = new Dictionary<string, ISynchronizable>();

            Component[] components = GetComponentsInChildren<Component>(true);
            foreach (Component component in components)
            {
                if (component is ISynchronizable saveable)
                {
                    _saveables.Add(saveable.Id, saveable);
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
                    //TODO: Cleanup garbage in save data
                    Debug.LogWarning($"Garbage object id: {pair.Key}");
                }
            }
        }
    }
}