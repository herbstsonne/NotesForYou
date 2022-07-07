using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesForYou.Core.Database;

namespace NotesForYou.Core.Notes
{
    public class NotesDataAccessor : INotesDataAccessor, IDisposable
    {
        private readonly NotesContext _context;

        public NotesDataAccessor(NotesContext context)
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

        public async Task<Note> GetRandomNote()
        {
            return await _context.Note.FirstOrDefaultAsync(x => x.Date == null);
        }

        public void UpdateNote(Note note)
        {
            note.Date = DateTime.Now;
            _context.Note.Update(note);
            _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}