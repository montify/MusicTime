namespace MusicTimeClient.Models.Entry
{
    public class Entry
    {
        public Entry()
        {
            
        }
        public Entry(string description, int durationInMinutes)
        {
            Description = description;
            DurationInMinutes = durationInMinutes;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public int DurationInMinutes { get; set; }
        public bool IsFinished { get; set; }
        public string Description { get; set; }
        
    }
}
