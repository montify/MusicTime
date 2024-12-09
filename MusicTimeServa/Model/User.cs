namespace MusicTimeServa.Model
{
    public class User
    {
        public int Id { get; set; } = 0;
        public string Name{ get; set; }
        public string Email{ get; set; } 
        public string Token{ get; set; } = string.Empty;
        public int Password { get; set; }
    }

    public class UserRegisterRequestDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Password { get; set; }
    }

    public class UserRegisterResponseDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }      
    }

    public class UserLoginRequestDTO
    {
        public string Email { get; set; }
        public int Password { get; set; }
    }

    public class UserLoginResponseDTO
    {
        public int Id { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
