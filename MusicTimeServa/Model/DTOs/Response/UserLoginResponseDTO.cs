﻿namespace MusicTimeServa.Model.DTOs.Response
{
    public class UserLoginResponseDTO
    {
        public int Id { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
