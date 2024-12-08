using MusicTimeClient.Contracts;
using MusicTimeClient.Models.Entry;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace MusicTimeClient.Services
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient m_httpClient;

        public EntryService(HttpClient httpClient)
        {
            m_httpClient = httpClient;
        }

        public async Task<Entry[]> GetAllEntries() => await m_httpClient.GetFromJsonAsync<Entry[]>("Entry") ?? [];
        public async Task DeleteEntry(int entryId) => await m_httpClient.DeleteAsync($"https://localhost:7207/Entry?id={entryId}");
        public async Task AddEntry(Entry entry) => await m_httpClient.PostAsJsonAsync("Entry", entry);
        public async Task UpdateEntry(Entry entry) => await m_httpClient.PutAsJsonAsync("Entry", entry);
    }
}