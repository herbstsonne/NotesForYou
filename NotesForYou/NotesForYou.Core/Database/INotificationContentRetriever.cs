using System.Threading.Tasks;
using NotesForYou.Core.AllEntries;
using NotesForYou.Core.ShowMessage;

namespace NotesForYou.Core.Database
{
    public interface INotificationContentRetriever
    {
        AllNoteEntriesDataAccessor DataAccessor { get; set; }

        Task<Note> SelectNote();
    }
}