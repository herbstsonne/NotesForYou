using System;
using Xamarin.Forms;

namespace NotesForYou.Core.NewEntries
{
    public class NewNoteViewModel : BaseViewModel
    {
        private DateTime _day;
        private string _headline;
        private string _link;
        private Category _category;
        private INewEntryDataAccessor _newEntryhandler;

        public DateTime Day
        {
            get => _day;
            set => SetProperty(ref _day, value);
        }

        public string Headline
        {
            get => _headline;
            set => SetProperty(ref _headline, value);
        }

        public string Link
        {
            get => _link;
            set => SetProperty(ref _link, value);
        }

        public Category Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        
        public NewNoteViewModel()
        {
            _newEntryhandler = new NewEntryDataAccessor(_noteContext);
            
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return _newEntryhandler.Validate(_headline, _link);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var note = NoteEntryFactory.Create(Category, Headline, Link);

            _newEntryhandler.Save(note);

            // This will pop the current page off the navigation stack
            if(Shell.Current == null)
                return;
            await Shell.Current.GoToAsync("..");
        }
    }
}
