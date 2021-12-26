using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using NotesForYou.Core.NewEntries;
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

        public NotesViewModel()
        {
            Title = "All shown notes";
            Entries = new ObservableCollection<Note>();
            LoadEntriesCommand = new Command(() => ExecuteLoadItemsCommand());

            AddEntryCommand = new Command(OnAddItem);

            _dataAccessor = new AllNoteEntriesDataAccessor(_noteContext);
        }
        

        private void ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Entries.Clear();
                var currentEntries = _dataAccessor.GetAll(new List<Note>());
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