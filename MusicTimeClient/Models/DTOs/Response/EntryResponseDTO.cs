namespace MusicTimeClient.Models.DTOs.Response
{
    public class EntryResponseDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DurationInMinutes { get; set; }
        public bool IsFinished { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
