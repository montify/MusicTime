using AutoMapper;
using MusicTimeClient.Contracts;
using MusicTimeClient.Models;
using MusicTimeClient.Models.DTOs.Response;
using System.Net.Http.Json;

namespace MusicTimeClient.Services
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient m_httpClient;
        private readonly IMapper m_Mapper;

        public EntryService(HttpClient httpClient, IMapper mapper)
        {
            m_httpClient = httpClient;
            m_Mapper = mapper;
        }

        public async Task<Entry[]> GetAllEntries()
        {
            try
            {
                var response = await m_httpClient.GetFromJsonAsync<ApiResponse<EntryResponseDTO[]>>("Entry");
                if (response is null || !response.IsSuccess || response.Payload is null)
                    return Array.Empty<Entry>();

                var entrys = m_Mapper.Map<EntryResponseDTO[], Entry[]>(response.Payload);

                return entrys;
            }
            catch (Exception)
            {
                return Array.Empty<Entry>();
            }
        }

        public async Task DeleteEntry(int entryId) => await m_httpClient.DeleteAsync($"https://localhost:7207/Entry?id={entryId}");
        public async Task AddEntry(Entry entry) => await m_httpClient.PostAsJsonAsync("Entry", entry);
        public async Task UpdateEntry(Entry entry) => await m_httpClient.PutAsJsonAsync("Entry", entry);   
    }
}