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
}
