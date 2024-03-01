using Serializable;

public interface ISaveable
{
    public void SaveData(Entry entry);

    public void LoadData(Entry entry);

    public void OnLoaded();

    public string GetId { get; }
}