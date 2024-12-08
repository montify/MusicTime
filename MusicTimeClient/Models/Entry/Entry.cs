﻿namespace MusicTimeClient.Models.Entry
{
    public class Entry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public int DurationInMinutes { get; set; }
        public bool IsFinished { get; set; }
        public string Description { get; set; }
        public string DateToString() => Date.ToString("dd/MM/yy/").Trim('.');

        public Entry()
        {
        }
        public Entry(string description, int durationInMinutes)
        {
            Description = description;
            DurationInMinutes = durationInMinutes;
        }
    }
}