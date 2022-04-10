using System;
using Xamarin.Forms;

namespace NotesForYou.Core.NewEntries
{
    public class NewNoteDataAccessor : INewNoteDataAccessor
    {
        private readonly NotesContext _context;

        public NewNoteDataAccessor()
        {
            _context = DependencyService.Resolve<NotesContext>();
        }

        public bool Validate(string headline, string link, Category category)
        {
            return !String.IsNullOrWhiteSpace(headline)
                   && !String.IsNullOrWhiteSpace(link) && (int)category != -1;
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