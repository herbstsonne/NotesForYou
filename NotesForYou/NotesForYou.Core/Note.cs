using System;

namespace NotesForYou.Core
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Headline { get; set; }
        public string Link { get; set; }
        public int Category { get; set; }
        public DateTime? Date { get; set; }
    }
}