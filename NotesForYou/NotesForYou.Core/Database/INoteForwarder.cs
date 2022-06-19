using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NotesForYou.Core.AllEntries;
using NotesForYou.Core.ShowMessage;

namespace NotesForYou.Core.Database
{
    public interface INoteForwarder
    {
        Task<Note> GetUpdatedNote();
        Task DisplayNotification();
        Task ShowAllEntries(ObservableCollection<Note> entries);
    }
}