using System;
using System.Collections.Generic;
using System.Linq;

namespace NotesForYou.Core.AllEntries
{
    public class AllNoteEntriesDataAccessor : IAllNoteEntriesDataAccessor
    {
        private readonly NotesContext _context;

        public AllNoteEntriesDataAccessor(NotesContext context)
        {
            _context = context;
        }

        public List<Note> GetAll(List<Note> entries)
        {
            var items = _context.Note.Where(x => x.Date != null).ToList(); 
            items.Sort((a, b) => DateTime.Compare((DateTime)a.Date, (DateTime)b.Date));

            foreach (var item in items)
            {
                entries.Add(item);
            }

            return entries;
        }

        public Note GetLatestNote()
        {
            var allEntries = GetAll(new List<Note>());
            return allEntries.FirstOrDefault();
        }
    }
}