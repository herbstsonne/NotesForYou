using System;

namespace NotesForYou.Core.NewEntries
{
    public class NewEntryDataAccessor : INewEntryDataAccessor
    {
        private readonly NotesContext _context;

        public NewEntryDataAccessor(NotesContext context)
        {
            _context = context;
        }

        public bool Validate(string headline, string dailyThoughtsText)
        {
            return !String.IsNullOrWhiteSpace(headline)
                   && !String.IsNullOrWhiteSpace(dailyThoughtsText);
        }
        
        public void Save(Note entry)
        {
            _context.Note.Add(entry);
            _context.SaveChanges();
        }
    }
}