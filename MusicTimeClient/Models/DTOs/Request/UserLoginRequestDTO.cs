﻿using System.ComponentModel.DataAnnotations;

namespace MusicTimeClient.Models.DTOs.Request
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