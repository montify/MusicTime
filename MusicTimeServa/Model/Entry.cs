namespace MusicTimeServa.Model
{
    public class Entry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public int DurationInMinutes { get; set; }
        public bool IsFinished { get; set; }
        public string Description { get; set; }

        public Entry(string description, int durationInMinutes)
        {
            Description = description;
            DurationInMinutes = durationInMinutes;
        }

        public bool CheckIfValid()
        {
            if (DurationInMinutes <= 0 || string.IsNullOrEmpty(Description))
                return false;

            return true;
        }
    }

    public class AddEntryRequestDTO()
    {
        public int DurationInMinutes { get; set; }
        public string Description { get; set; }
    }

}