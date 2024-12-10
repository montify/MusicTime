using System.ComponentModel.DataAnnotations;

namespace MusicTimeServa.Model.DTOs.Request
{
    public class UserLoginRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Password { get; set; }
    }
}
