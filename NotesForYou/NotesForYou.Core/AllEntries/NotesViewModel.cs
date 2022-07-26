using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NotesForYou.Core.Database;
using NotesForYou.Core.NewEntries;
using NotesForYou.Core.ShowMessage;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NotesForYou.Core.AllEntries
{
    public class NotesViewModel : BaseViewModel
    {
        private readonly AllNoteEntriesDataAccessor _dataAccessor;
        private Note _selectedItem;

        public ObservableCollection<Note> Entries { get; }
        public Command LoadEntriesCommand { get; }
        public Command AddEntryCommand { get; }
        public Command ClickLinkCommand { get; }

        public NotesViewModel()
        {
            Title = "Alle bereits angezeigten Nachrichten";
            Entries = new ObservableCollection<Note>();
            LoadEntriesCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ClickLinkCommand = new Command(async () => await ExecuteClickLinkCommand());

            AddEntryCommand = new Command(OnAddItem);

            var contentRetriever = DependencyService.Resolve<INotificationContentRetriever>();

            _dataAccessor = contentRetriever.DataAccessor;
        }

        private async Task ExecuteClickLinkCommand()
        {
            var note = SelectedItem;
            if (note == null)
                return;
            var usersLink = note.Link;
            Uri uri;
            if(usersLink.StartsWith("http"))
                uri = new System.Uri(usersLink);
            else if(usersLink.StartsWith("www"))
            {
                uri = new Uri("http://" + usersLink);
            }
            else
            {
                uri = new Uri("https://www.google.com/search?q=" + usersLink);
            }

            var success = await Launcher.TryOpenAsync(uri);
            if (!success)
            {
                // log and show error
            }
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Entries.Clear();
                List<Note> currentEntries = await _dataAccessor.GetAll();
                foreach (var entry in currentEntries)
                {
                    Entries.Add(entry);
                }

                SelectedItem = SelectedItem == null ? currentEntries.FirstOrDefault() : SelectedItem;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Note SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewEntryPage));
        }
    }
}