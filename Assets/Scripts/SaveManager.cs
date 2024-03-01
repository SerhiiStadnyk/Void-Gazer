using System;
using System.Collections.Generic;
using Serializable;
using UnityEngine;
using UnityEngine.Serialization;

public class SaveManager : MonoBehaviour, IInitable
{
    private List<SaveFile> _saveFiles = new List<SaveFile>();
    private List<ISaveable> _saveables = new List<ISaveable>();

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


    public void RegisterSaveable(ISaveable saveable)
    {
        _saveables.Add(saveable);
    }


    public void UnregisterSaveable(ISaveable saveable)
    {
        _saveables.Remove(saveable);
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

        saveFile.InitSave(_saveables.Count, saveName);
        foreach (ISaveable saveable in _saveables)
        {
            Entry entry = new Entry(saveable.GetId);
            saveable.SaveData(entry);
            entry.Serialize();
            saveFile.Register(entry);
        }

        saveFile.Serialize();
        UpdateSaveFiles();
    }


    private void Load(SaveFile saveFile)
    {
        saveFile.Deserialize();

        foreach (ISaveable saveable in _saveables)
        {
            if (saveFile.Entries.ContainsKey(saveable.GetId))
            {
                Entry entry = saveFile.Entries[saveable.GetId];
                entry.Deserialize();
                saveable.LoadData(entry);
            }
            else
            {
                //TODO: Dispose objects that are not saved since they been disposed during gameplay
                Debug.LogWarning("Dispose object");
            }
        }

        //TODO: Instantiate and load new objects since they where instantiated during gameplay
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
        _entries.AddOrUpdate(entry.EntryId, entry);
    }


    public void Serialize()
    {
        _entries.OnBeforeSerialize();
    }


    public void Deserialize()
    {
        _entries.OnAfterDeserialize();
    }
}
