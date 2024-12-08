using Microsoft.IdentityModel.Tokens;
using MusicTimeServa.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MusicTimeServa.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;
        private IConfiguration _configuration;

        public UserService(DataContext dbContext, IConfiguration configuration)
        {
            _context = dbContext;
            _configuration = configuration;
        }

        public void Register(User user)
        {
            _context.Add<User>(user);
            _context.SaveChanges();
        }

        public User? Login(UserLoginDTO loginDTO)
        {
            var user = ValidateUserAndPassword(loginDTO);

            if(user != null)
            {
                var token = SetJwtTokenToUser(user);
                user.Token = new JwtSecurityTokenHandler().WriteToken(token);

                return user;
            }

            return null;
        }

        private User? ValidateUserAndPassword(UserLoginDTO userLoginDTO)
        {
            var user = _context.Set<User>().Where(u => userLoginDTO.Email == u.Email && userLoginDTO.Password == u.Password).ToList().FirstOrDefault();

            if (user is not null)
                return user;

            return null;
        }

        private SecurityToken SetJwtTokenToUser(User user)
        {
            var issuer = _configuration.GetSection("Jwt:Issuer").Get<string>();
            var durationInMinute = _configuration.GetSection("Jwt:DurationInMinutes").Get<int>();

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Get<string>()));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
               issuer: issuer,
               audience: null,
               claims: null,
               expires: DateTime.UtcNow.AddMinutes(durationInMinute),
               signingCredentials: signingCredentials);

            
            return jwtSecurityToken;
        }
    }
}
