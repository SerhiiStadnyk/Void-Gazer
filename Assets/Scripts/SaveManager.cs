using System;
using System.Collections.Generic;
using System.Linq;
using Serializable;
using Synchronizable;
using UnityEngine;

public class SaveManager : MonoBehaviour, IInitable
{
    [SerializeField]
    private IdManager _idManager;

    private SaveFile _sessionSaveFile;

    private List<SaveFile> _saveFiles = new List<SaveFile>();
    private List<SceneSyncHandler> _sceneSyncHandlers = new List<SceneSyncHandler>();

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

        _sessionSaveFile ??= new SaveFile();
    }


    public void RegisterSyncHandler(SceneSyncHandler lifetimeHandler)
    {
        _sceneSyncHandlers.Add(lifetimeHandler);
    }


    public void UnregisterSyncHandler(SceneSyncHandler lifetimeHandler)
    {
        _sceneSyncHandlers.Remove(lifetimeHandler);
    }


    public void CreateSaveFile(string saveName)
    {
        SaveSessionData();
        SaveFile saveFile = new SaveFile(_sessionSaveFile);
        _saveFiles.Add(saveFile);
        CreateSaveFile(saveFile, saveName);
    }


    public void OverwriteSaveFile(SaveFile oldSaveFile)
    {
        SaveSessionData();
        SaveFile saveFile = new SaveFile(_sessionSaveFile);
        int oldFileIndex =_saveFiles.IndexOf(oldSaveFile);
        _saveFiles[oldFileIndex] = saveFile;
        CreateSaveFile(saveFile, oldSaveFile.Name);
    }


    public void DeleteSaveFile(SaveFile saveFile)
    {
        _saveFiles.Remove(saveFile);
        UpdateSaveFiles();
    }


    public void LoadSceneData(SceneSyncHandler sceneSyncHandler)
    {
        if (_sessionSaveFile != null && _sessionSaveFile.Entries != null && _sessionSaveFile.Entries.Count() != 0)
        {
            ISynchronizable sceneDataHandlerSynchronizable = sceneSyncHandler;
            if (_sessionSaveFile.Entries.ContainsKey(sceneDataHandlerSynchronizable.InstanceId))
            {
                Entry sceneDataEntry = _sessionSaveFile.Entries[sceneDataHandlerSynchronizable.InstanceId];
                sceneDataEntry.Deserialize();
                sceneDataHandlerSynchronizable.LoadData(sceneDataEntry);
            }
        }
    }


    public void SaveObjectData(ISynchronizable synchronizable)
    {
        Entry objectDataEntry = new Entry(synchronizable.InstanceId);
        synchronizable.SaveData(objectDataEntry);
        _sessionSaveFile.Register(objectDataEntry);
    }


    private void SaveSessionData()
    {
        SaveGlobalData();
        foreach (SceneSyncHandler sceneSyncHandler in _sceneSyncHandlers)
        {
            SaveObjectData(sceneSyncHandler);
        }
    }


    private void SaveGlobalData()
    {
        //TODO: Improve this
        SaveObjectData(_idManager);
    }


    private void CreateSaveFile(SaveFile saveFile, string saveName = null)
    {
        saveFile.InitSave(0, saveName);
        saveFile.Serialize();
        UpdateSaveFiles();
    }


    public void SaveSceneData(SceneSyncHandler sceneSync)
    {
        ISynchronizable sceneDataSynchronizable = sceneSync;
        Entry sceneDataEntry = new Entry(sceneDataSynchronizable.InstanceId);
        sceneDataSynchronizable.SaveData(sceneDataEntry);
        _sessionSaveFile.Register(sceneDataEntry);
    }


    public void InitLoading(SaveFile saveFile)
    {
        saveFile.Deserialize();
        _sessionSaveFile = saveFile;
        //TODO: Load active scene in save file
    }


    private void UpdateSaveFiles()
    {
        string data = JsonUtility.ToJson(new SaveFilesWrapper(_saveFiles));
        PlayerPrefs.SetString(SAVE_FILES_ID, data);
        PlayerPrefs.Save();
    }


    [Serializable]
    private class SaveFilesWrapper
    {
        [SerializeField]
        public List<SaveFile> _saveFiles;

        public SaveFilesWrapper(List<SaveFile> saveFiles)
        {
            _saveFiles = saveFiles;
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


    public SaveFile()
    {
        _entries = new SerializableDictionary<string, Entry>();
    }


    public SaveFile(SaveFile value)
    {
        _name = value.Name;
        _entries = new SerializableDictionary<string, Entry>(value.Entries.Dictionary);
        _dateTime = value.DateTime;
    }


    public void InitSave(int count, string name)
    {
        _name = name;
        _dateTime = DateTime.Now;
        _entries ??= new SerializableDictionary<string, Entry>(count);
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
