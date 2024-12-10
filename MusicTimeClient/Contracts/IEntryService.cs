using MusicTimeClient.Models;
using MusicTimeClient.Models.DTOs.Request;

namespace MusicTimeClient.Contracts
{
    public interface IEntryService
    {
        public Task<Entry[]>? GetAllEntries();
        public Task DeleteEntry(int entryId);
        public Task AddEntry(Entry entry);
        public Task UpdateEntry(Entry entry);
    }
}