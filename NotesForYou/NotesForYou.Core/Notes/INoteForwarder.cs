using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace NotesForYou.Core.Notes
{
    public interface INoteForwarder
    {
        Task<Note> GetUpdatedNote();
        Task DisplayNotification();
        Task ShowAllEntries(ObservableCollection<Note> entries);
    }
}