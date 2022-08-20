using NotesForYou.Core.ShowMessage;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NotesForYou.Core.AllEntries
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
                    Console.WriteLine(InfoMessageHandler.GetNoteNotAvailableText());
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
                    DependencyService.Get<INotificationManager>().SendNotification(InfoMessageHandler.GetNoteNotAvailableText(), "");
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

        public async Task ShowAllNotes(ObservableCollection<Note> notes)
        {
            try
            {
                notes.Clear();
                var dbNotes = await _dataAccessor.GetAll();
                foreach (var dbNote in dbNotes)
                {
                    notes.Add(dbNote);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
