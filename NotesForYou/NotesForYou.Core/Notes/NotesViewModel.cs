using System;
using System.Collections.ObjectModel;
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
                uri = new Uri(usersLink);
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
                Console.WriteLine($"Could not open link: {uri}");
            }
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            await _noteForwarder.ShowAllNotes(Entries);
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
            await NotesForYouNavigation.NavigateTo(new NewEntryPage());
        }
    }
}