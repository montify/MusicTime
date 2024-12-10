using System.ComponentModel.DataAnnotations;

namespace MusicTimeServa.Model.DTOs.Request
{
    public class UserRegisterRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int Password { get; set; }
    }
}
