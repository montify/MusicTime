namespace MusicTimeServa
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Payload { get; set; }
    }
}