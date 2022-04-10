using System;
using NotesForYou.Core.AllEntries;
using NotesForYou.Core.Login;
using NotesForYou.Core.NewEntries;
using NotesForYou.Core.Settings;
using Xamarin.Forms;

namespace NotesForYou.Core
{
    public partial class AppShell : Shell
    {

        private INotificationManager _notificationManager;
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(EntriesPage), typeof(EntriesPage));
            Routing.RegisterRoute(nameof(NewEntryPage), typeof(NewEntryPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

            _notificationManager = DependencyService.Get<INotificationManager>();
            _notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
            });
        }
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("//LoginPage");
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("//SettingsPage");
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}
