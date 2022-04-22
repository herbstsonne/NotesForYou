using System.Collections.Generic;
using System.Threading.Tasks;
using NotesForYou.Core.ShowMessage;

namespace NotesForYou.Core.AllEntries
{
    public interface IAllNoteEntriesDataAccessor
    {
        Task<List<Note>> GetAll();
        Task<Note> GetLatestNote();
        Task<Note> GetRandomNote();
        void UpdateNote(Note note);
    }
}