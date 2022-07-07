using System;
using System.Collections.Generic;
using NotesForYou.Core.ShowNote;
using Xamarin.Forms;

namespace NotesForYou.Core.AddNote
{
    public class AddNoteViewModel : BaseViewModel
    {
        private DateTime _day;
        private string _headline;
        private string _link;
        private Category _category;
        private IAddNoteDataAccessor _newEntryhandler;

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

        public Category SelectedCategory
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public AddNoteViewModel()
        {
            _newEntryhandler = new AddNoteDataAccessor();

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);

            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return _newEntryhandler.Validate(_headline, _link, _category);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var note = AddNoteFactory.Create((int)SelectedCategory, Headline, Link);

            _newEntryhandler.Save(note);

            if (Shell.Current == null)
                return;
            await Shell.Current.GoToAsync("..");
        }
    }
}
