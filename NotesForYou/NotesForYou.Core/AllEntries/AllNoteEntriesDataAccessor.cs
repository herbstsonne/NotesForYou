using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesForYou.Core.AllEntries
{
    public class AllNoteEntriesDataAccessor : IAllNoteEntriesDataAccessor, IDisposable
    {
        private readonly NotesContext _context;

        public AllNoteEntriesDataAccessor(NotesContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> GetAll()
        {
            var entries = new List<Note>();
            var items = await _context.Note.Where(x => x.Date != null).ToListAsync(); 
            items.Sort((a, b) => DateTime.Compare((DateTime)a.Date, (DateTime)b.Date));

            foreach (var item in items)
            {
                entries.Add(item);
            }

            return entries;
        }

        public async Task<Note> GetLatestNote()
        {
            var latestNote = await _context.Note.FirstOrDefaultAsync(x => x.Date != null);
            return latestNote;
        }

        public async Task<Note> GetRandomNote()
        {
            return await _context.Note.FirstOrDefaultAsync(x => x.Date == null);
        }

        public void UpdateNote(Note note)
        {
            note.Date = DateTime.Now;
            _context.Note.Update(note);
        }
        
        public void Dispose()
        {
        }
    }
}