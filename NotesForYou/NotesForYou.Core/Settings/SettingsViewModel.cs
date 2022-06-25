using NotesForYou.Core.AllEntries;
using System;
using Xamarin.Forms;

namespace NotesForYou.Core.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        private ISettingsDataAccessor _dataAccessor;
        private TimeSpan difference;

        private TimeSpan _showTime;

        public TimeSpan ShowTime
        {
            get => _showTime;
            set => SetProperty(ref _showTime, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public SettingsViewModel()
        {
            _dataAccessor = (ISettingsDataAccessor)App.ServiceProvider.GetService(typeof(ISettingsDataAccessor));

            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            difference = new TimeSpan(0, 0, 15);
        }

        private async void OnCancel()
        {
            await NotesForYouNavigation.NavigateTo(new EntriesPage());
        }

        private async void OnSave()
        {
            var setting = SettingsFactory.Create(difference, _showTime);

            await _dataAccessor.Save(setting);
            await NotesForYouNavigation.NavigateTo(new EntriesPage());
            await SettingsNotifier.ShowNotificationInDefinedTimes?.Invoke();
        }
    }
}
