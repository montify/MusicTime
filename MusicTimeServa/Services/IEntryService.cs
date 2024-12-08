using MusicTimeServa.Model;

namespace MusicTimeServa.Services
{
    public interface IEntryService
    {
        public List<Entry> GetAllEntrys();
        public void AddEntry(Entry entry);
        public void DeleteEntryFromId(int entryId);
        public void UpdateUser(Entry modifiedEntry);
        
    }
}
