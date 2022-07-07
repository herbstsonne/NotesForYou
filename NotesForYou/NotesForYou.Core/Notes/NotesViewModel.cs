using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NotesForYou.Core.AddNote;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NotesForYou.Core.Notes
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
            ClickLinkCommand = new Command<string>(async url => await ExecuteClickLinkCommand(url));

            AddEntryCommand = new Command(OnAddItem);

            _noteForwarder = (INoteForwarder)App.ServiceProvider.GetService(typeof(INoteForwarder));
        }

        private async Task ExecuteClickLinkCommand(string url)
        {
            await Launcher.OpenAsync(new Uri(url));
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
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddNotePage));
        }
    }
}