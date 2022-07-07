using System;
using NotesForYou.Core.Database;

namespace NotesForYou.Core.AddNote
{
    public class AddNoteDataAccessor : IAddNoteDataAccessor
    {
        private readonly NotesContext _context;

        public AddNoteDataAccessor()
        {
            _context = (NotesContext)App.ServiceProvider.GetService(typeof(NotesContext));
        }

        public bool Validate(string headline, string link, Category category)
        {
            return !string.IsNullOrWhiteSpace(headline)
                   && !string.IsNullOrWhiteSpace(link) && (int)category != -1;
        }

        public void Save(Note entry)
        {
            try
            {
                _context.Note.Add(entry);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine("Could not save note: " + e.Message);
#endif
            }
        }
    }
}