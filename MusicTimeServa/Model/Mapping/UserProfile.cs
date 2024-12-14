using AutoMapper;
using MusicTimeServa.Model.DTOs.Request;
using MusicTimeServa.Model.DTOs.Response;

namespace MusicTimeServa.Model.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterRequestDTO, User>();
            CreateMap<User, UserRegisterResponseDTO>();
            CreateMap<UserLoginRequestDTO, User>();
            CreateMap<User, UserLoginRequestDTO>();
            CreateMap<User, UserLoginResponseDTO>();
        }
    }
}
