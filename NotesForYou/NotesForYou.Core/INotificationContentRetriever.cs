using NotesForYou.Core.AllEntries;
using System.Threading.Tasks;

namespace NotesForYou.Core
{
    public interface INotificationContentRetriever
    {
        AllNoteEntriesDataAccessor DataAccessor { get; set; }

        Task<Note> SelectNote();
    }
}