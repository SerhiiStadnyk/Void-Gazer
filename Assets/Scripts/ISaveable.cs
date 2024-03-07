using Serializable;

public interface ISaveable: IIdHolder
{
    public void SaveData(Entry entry);

    public void LoadData(Entry entry);
}