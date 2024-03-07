using System;
using System.Collections.Generic;
using System.Linq;
using Serializable;
using UnityEngine;

public class SaveManager : MonoBehaviour, IInitable
{
    [SerializeField]
    private IdManager _idManager;

    private SaveFile _sessionSaveFile;

    private List<SaveFile> _saveFiles = new List<SaveFile>();
    private List<SceneLifetimeHandler> _lifetimeHandlers = new List<SceneLifetimeHandler>();

    private const string SAVE_FILES_ID = "SaveFiles";

    public List<SaveFile> SaveFiles => _saveFiles;


    void IInitable.Init()
    {
        string data = PlayerPrefs.GetString(SAVE_FILES_ID, null);
        if (!string.IsNullOrEmpty(data))
        {
            Debug.LogWarning(data);

            SaveFilesWrapper wrapper = JsonUtility.FromJson<SaveFilesWrapper>(data);
            _saveFiles = wrapper._saveFiles;
        }
    }


    public void RegisterLifetimeHandler(SceneLifetimeHandler lifetimeHandler)
    {
        _lifetimeHandlers.Add(lifetimeHandler);
    }


    public void UnregisterLifetimeHandler(SceneLifetimeHandler lifetimeHandler)
    {
        _lifetimeHandlers.Remove(lifetimeHandler);
    }


    public void CreateSaveFile(string saveName)
    {
        SaveFile saveFile = new SaveFile();
        _saveFiles.Add(saveFile);
        Save(saveFile, saveName);
    }


    public void DeleteSaveFile(SaveFile saveFile)
    {
        _saveFiles.Remove(saveFile);
        UpdateSaveFiles();
    }


    public void Save(SaveFile saveFile, string saveName = null)
    {
        saveName ??= saveFile.Name;

        int saveablesCount = _lifetimeHandlers.SelectMany(handler => handler.Saveables).Count();
        saveFile.InitSave(saveablesCount, saveName);

        ISaveable idManager = _idManager;
        Entry idManagerEntry = new Entry(idManager.Id);
        idManager.SaveData(idManagerEntry);
        saveFile.Register(idManagerEntry);

        foreach (SceneLifetimeHandler lifetimeHandler in _lifetimeHandlers)
        {
            foreach (ISaveable saveable in lifetimeHandler.Saveables)
            {
                Entry entry = new Entry(saveable.Id);
                saveable.SaveData(entry);
                saveFile.Register(entry);
            }
        }

        saveFile.Serialize();
        UpdateSaveFiles();
    }


    public void InitLoading(SaveFile saveFile)
    {
        saveFile.Deserialize();
        _sessionSaveFile = saveFile;
    }


    public void LoadSceneData(List<ISaveable> saveables)
    {
        if (_sessionSaveFile != null)
        {
            foreach (ISaveable saveable in saveables)
            {
                if (_sessionSaveFile.Entries.ContainsKey(saveable.Id))
                {
                    Entry entry = _sessionSaveFile.Entries[saveable.Id];
                    entry.Deserialize();
                    saveable.LoadData(entry);
                }
                else
                {
                    //TODO: Dispose objects that are not saved since they been disposed during gameplay
                    Debug.LogWarning("Dispose object");
                }
            }
        }
    }


    private void UpdateSaveFiles()
    {
        string data = JsonUtility.ToJson(new SaveFilesWrapper(_saveFiles));
        PlayerPrefs.SetString(SAVE_FILES_ID, data);
        PlayerPrefs.Save();
    }


    [System.Serializable]
    private class SaveFilesWrapper
    {
        [SerializeField]
        public List<SaveFile> _saveFiles;

        public SaveFilesWrapper(List<SaveFile> saveFiles)
        {
            this._saveFiles = saveFiles;
        }
    }
}


[Serializable]
public class SaveFile
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private SerializableDictionary<string, Entry> _entries;
    [SerializeField]
    private DateTime _dateTime;

    public string Name => _name;

    public SerializableDictionary<string, Entry> Entries => _entries;

    public DateTime DateTime => _dateTime;


    public void InitSave(int count, string name)
    {
        _name = name;
        _dateTime = DateTime.Now;

        _entries?.Clear();
        _entries = new SerializableDictionary<string, Entry>(count);
    }


    public void Register(Entry entry)
    {
        entry.Serialize();
        _entries.AddOrUpdate(entry.EntryId, entry);
    }


    public void Serialize()
    {
        _entries.Serialize();
    }


    public void Deserialize()
    {
        _entries.Deserialize();
    }
}
