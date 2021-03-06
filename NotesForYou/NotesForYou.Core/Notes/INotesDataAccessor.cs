using System.Collections.Generic;
using System.Threading.Tasks;
using NotesForYou.Core.ShowMessage;

namespace NotesForYou.Core.AllEntries
{
    public interface INotesDataAccessor
    {
        Task<List<Note>> GetAll();
        Task<Note> GetRandomNote();
        void UpdateNote(Note note);
    }
}