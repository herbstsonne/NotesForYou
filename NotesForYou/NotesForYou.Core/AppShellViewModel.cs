using NotesForYou.Core.AllEntries;
using NotesForYou.Core.Login;
using NotesForYou.Core.Settings;
using Xamarin.Forms;

namespace NotesForYou.Core
{
    public class AppShellViewModel
    {
        public Command ClickLogout { get; set; }
        public Command ClickSettings { get; set; }
        public Command ClickOverview { get; set; }

        public AppShellViewModel()
        {
            ClickLogout = new Command(OnClickLogout);
            ClickSettings = new Command(OnClickSettings);
            ClickOverview = new Command(OnClickOverview);
        }

        private async void OnClickLogout(object obj)
        {
            await NotesForYouNavigation.NavigateTo(new LoginPage());
        }

        private async void OnClickSettings(object obj)
        {
            await NotesForYouNavigation.NavigateTo(new SettingsPage());
        }

        private async void OnClickOverview(object obj)
        {
            await NotesForYouNavigation.NavigateTo(new EntriesPage());
        }
    }
}
