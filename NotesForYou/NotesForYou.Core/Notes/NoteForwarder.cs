using NotesForYou.Core.ShowNote;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using static NotesForYou.Core.Extension.MessageExtension;

namespace NotesForYou.Core.Notes
{
    public class NoteForwarder : INoteForwarder
    {
        private readonly INotesDataAccessor _dataAccessor;

        public NoteForwarder(INotesDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        public async Task<Note> GetUpdatedNote()
        {
            try
            {
                var note = await _dataAccessor.GetRandomNote();
                if (note == null)
                {
                    Console.WriteLine(new Note().GetNoteNotAvailableText());
                    return null;
                }

                _dataAccessor.UpdateNote(note);
                return note;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task DisplayNotification()
        {
            try
            {
                var note = await GetUpdatedNote();
                if (note == null)
                {
                    DependencyService.Get<INotificationManager>().SendNotification(new Note().GetNoteNotAvailableText(), "");
                }
                else
                {
                    DependencyService.Get<INotificationManager>().SendNotification(note.Headline, note.Link);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task ShowAllEntries(ObservableCollection<Note> entries)
        {
            try
            {
                entries.Clear();
                var currentEntries = await _dataAccessor.GetAll();
                foreach (var entry in currentEntries)
                {
                    entries.Add(entry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
