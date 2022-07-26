using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NotesForYou.Core.NewEntries;
using NotesForYou.Core.ShowMessage;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NotesForYou.Core.AllEntries
{
    public class NotesViewModel : BaseViewModel
    {
        private readonly INoteForwarder _noteForwarder;
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

            _noteForwarder = (INoteForwarder)App.ServiceProvider.GetService(typeof(INoteForwarder));
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
            await _noteForwarder.ShowAllEntries(Entries);
            IsBusy = false;
            
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