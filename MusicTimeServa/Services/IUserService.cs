using MusicTimeServa.Model;

namespace MusicTimeServa.Services
{
    public interface IUserService
    {
        public void Register(User user); 
        public User? Login(UserLoginDTO loginDTO);
    }
}
