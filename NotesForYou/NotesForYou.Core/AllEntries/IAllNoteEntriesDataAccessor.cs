using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesForYou.Core.AllEntries
{
    public interface IAllNoteEntriesDataAccessor
    {
        List<Note> GetAll(List<Note> entries);
        Note GetLatestNote();
    }
}