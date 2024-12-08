using Microsoft.IdentityModel.Tokens;
using MusicTimeServa.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

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

            if (user != null)
            {
                var token = GenerateJwtToken(user);
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

        private SecurityToken GenerateJwtToken(User user)
        {
            var issuer = _configuration.GetSection("Jwt:Issuer").Get<string>();
            var durationInMinute = _configuration.GetSection("Jwt:DurationInMinutes").Get<int>();

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"])
            };

            var jwtSecurityToken = new JwtSecurityToken(
               issuer: issuer,
               audience: _configuration["Jwt:Audience"],
               claims: claims,
               expires: DateTime.UtcNow.AddMinutes(durationInMinute),
               signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
