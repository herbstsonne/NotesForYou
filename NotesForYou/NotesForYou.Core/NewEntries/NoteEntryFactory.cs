using System;

namespace NotesForYou.Core.NewEntries
{
    public static class NoteEntryFactory
    {
        public static Note Create(int category, string headline, string dailyThoughtsText)
        {
            return new Note()
            {
                Id = Guid.NewGuid(),
                Headline = headline,
                Link = dailyThoughtsText,
                Category = category
            };
        }
    }
}