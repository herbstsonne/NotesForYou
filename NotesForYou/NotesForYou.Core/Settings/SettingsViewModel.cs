using NotesForYou.Core.ShowTime;
using System;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using System.Windows.Input;
using System.Threading.Tasks;

namespace NotesForYou.Core.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        private SettingsDataAccessor _dataAccessor;
        private TimeSpan _showTime;
        private DateTime _minDate;
        private DateTime _maxDate;

        public DateTime MinDate
        {
            get => _minDate;
            set => SetProperty(ref _minDate, value);
        }
        public DateTime MaxDate
        {
            get => _maxDate;
            set => SetProperty(ref _maxDate, value);
        }

        public TimeSpan ShowTime
        {
            get => _showTime;
            set => SetProperty(ref _showTime, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public SettingsViewModel()
        {
            _dataAccessor = new SettingsDataAccessor(_noteContext);

            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            _showTime = new TimeSpan(9, 0, 0);
            _minDate = new DateTime(2021, 12, 24);
            _maxDate = DateTime.MaxValue;
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var setting = SettingsFactory.Create(ShowTime);

            await _dataAccessor.Save(setting);

            // This will pop the current page off the navigation stack
            if (Shell.Current == null)
                return;
            await Shell.Current.GoToAsync("..");
        }
    }
}
