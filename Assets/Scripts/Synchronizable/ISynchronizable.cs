using Serializable;

namespace Synchronizable
{
    public interface ISynchronizable: IIdHolder
    {
        public void SaveData(Entry entry);

        public void LoadData(Entry entry);
    }
}