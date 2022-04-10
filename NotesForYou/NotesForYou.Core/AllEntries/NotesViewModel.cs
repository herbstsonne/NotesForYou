using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using NotesForYou.Core.NewEntries;
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
            ClickLinkCommand = new Command<string>(async url => await ExecuteClickLinkCommand(url));

            AddEntryCommand = new Command(OnAddItem);

            var contentRetriever = DependencyService.Resolve<INotificationContentRetriever>();

            _dataAccessor = contentRetriever.DataAccessor;
        }

        private async Task ExecuteClickLinkCommand(string url)
        {
            await Launcher.OpenAsync(new System.Uri(url));
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Entries.Clear();
                var currentEntries = await _dataAccessor.GetAll();
                foreach (var entry in currentEntries)
                {
                    Entries.Add(entry);
                }
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
            await Shell.Current.GoToAsync(nameof(NewEntryPage));
        }

        //async void OnItemSelected(Note item)
        //{
        //    if (item == null)
        //        return;

        //    // This will push the ItemDetailPage onto the navigation stack
        //    await Shell.Current.GoToAsync($"{nameof(DailyEntryPage)}?{nameof(DailyEntryViewModel.ItemId)}={item.Id}");
        //}
    }
}