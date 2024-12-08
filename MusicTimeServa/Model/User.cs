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

    public class UserRegisterDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Password { get; set; }
    }

    public class UserLoginDTO
    {
        public string Email { get; set; }
        public int Password { get; set; }
    }
}
