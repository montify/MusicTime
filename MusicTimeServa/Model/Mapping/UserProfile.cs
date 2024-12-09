using AutoMapper;

namespace MusicTimeServa.Model.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //AuthController
            CreateMap<UserRegisterRequestDTO, User>();
            CreateMap<User, UserRegisterResponseDTO>();
            CreateMap<UserLoginRequestDTO, User>();
            CreateMap<User, UserLoginRequestDTO>();
            CreateMap<User, UserLoginResponseDTO>();

            //EntryController
            CreateMap<AddEntryRequestDTO, Entry>();
        }
    }
}
