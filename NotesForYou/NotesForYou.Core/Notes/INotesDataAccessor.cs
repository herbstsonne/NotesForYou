using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesForYou.Core.Notes
{
    public interface INotesDataAccessor
    {
        Task<List<Note>> GetAll();
        Task<Note> GetRandomNote();
        void UpdateNote(Note note);
    }
}