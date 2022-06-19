using NotesForYou.Core.AllEntries;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace NotesForYou.Core
{
    public interface INoteForwarder
    {
        Task<Note> GetUpdatedNote();
        Task DisplayNotification();
        Task ShowAllEntries(ObservableCollection<Note> entries);
    }
}