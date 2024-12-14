using System.ComponentModel.DataAnnotations;

namespace MusicTimeClient.Models.DTOs.Request
{
    public class UserRegisterRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
