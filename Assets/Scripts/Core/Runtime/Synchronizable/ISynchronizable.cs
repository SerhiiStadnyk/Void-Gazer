using Core.Runtime.Serializable;

namespace Core.Runtime.Synchronizable
{
    public interface ISynchronizable: IInstanceIdHolder
    {
        public void SaveData(Entry entry);

        public void LoadData(Entry entry);
    }
}