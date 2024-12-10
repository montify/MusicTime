using AutoMapper;
using MusicTimeClient.Models.DTOs.Response;

namespace MusicTimeClient.Models.Mapping
{
    public class EntryProfile : Profile
    {
        public EntryProfile()
        {
            CreateMap<EntryResponseDTO, Entry>();
        }
    }
}
