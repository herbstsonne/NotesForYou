using System;
using Xamarin.Forms;

namespace NotesForYou.Core.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        private SettingsDataAccessor _dataAccessor;
        private TimeSpan difference;
        private DateTime minDate;
        private DateTime maxDate;

        private DateTime _showTime;

        public DateTime ShowTime
        {
            get => _showTime;
            set => SetProperty(ref _showTime, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public SettingsViewModel()
        {
            _dataAccessor = DependencyService.Resolve<SettingsDataAccessor>();

            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            difference = new TimeSpan(0, 0, 15);
            minDate = DateTime.Now;
            maxDate = DateTime.MaxValue;
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var setting = SettingsFactory.Create(difference, minDate, maxDate);

            await _dataAccessor.Save(setting);

            // This will pop the current page off the navigation stack
            if (Shell.Current == null)
                return;
            await Shell.Current.GoToAsync("..");
        }
    }
}
