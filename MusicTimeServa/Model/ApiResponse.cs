namespace MusicTimeServa.Model
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Result { get; set; }
    }
}