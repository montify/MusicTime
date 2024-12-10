using AutoMapper;

namespace MusicTimeServa.Model.Mapping
{
    public class EntryProfile : Profile
    {
        public EntryProfile()
        {
            CreateMap<AddEntryRequestDTO, Entry>();
        }
    }
}
