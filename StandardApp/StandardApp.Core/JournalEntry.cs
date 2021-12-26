﻿using System;

namespace StandardApp.Core
{
    public class JournalEntry
    {
        public string Id { get; set; }
        public DateTime Day { get; set; }
        public string Headline { get; set; }
        public string DailyThoughtsText { get; set; }
        
        public bool CreatedByWidget { get; set; }
    }
}