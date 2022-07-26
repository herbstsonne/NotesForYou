using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NotesForYou.Core.ShowMessage;

namespace NotesForYou.Core.AllEntries
{
    public interface INoteForwarder
    {
        Task<Note> GetUpdatedNote();
        Task DisplayNotification();
        Task ShowAllEntries(ObservableCollection<Note> entries);
    }
}