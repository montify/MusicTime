using MusicTimeClient.Models.Entry;

namespace MusicTimeClient.Contracts
{
    public interface IEntryService
    {
        Task<Entry[]> GetAllEntries();
        public Task DeleteEntry(int entryId);
        public Task AddEntry(Entry entry);
        public Task UpdateEntry(Entry entry);
    }
}
