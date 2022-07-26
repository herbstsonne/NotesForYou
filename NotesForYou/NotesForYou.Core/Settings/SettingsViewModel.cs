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

        public Command LoadCommand { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public SettingsViewModel()
        {
            _dataAccessor = (ISettingsDataAccessor)App.ServiceProvider.GetService(typeof(ISettingsDataAccessor));

            LoadCommand = new Command(OnLoad);
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private async void OnLoad()
        {
            _showTime = await _dataAccessor.GetShowTime();
        }

        private async void OnCancel()
        {
            await NotesForYouNavigation.NavigateToMainPage();
        }

        private async void OnSave()
        {
            var setting = SettingsFactory.Create(difference, _showTime);

            await _dataAccessor.Save(setting);
            await NotesForYouNavigation.NavigateToMainPage();
            await (SettingsNotifier.ShowNotificationInDefinedTimes?.Invoke()).ConfigureAwait(false);
        }
    }
}
