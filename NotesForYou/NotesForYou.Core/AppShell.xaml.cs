using System;
using System.Threading.Tasks;
using NotesForYou.Core.AllEntries;
using NotesForYou.Core.Login;
using NotesForYou.Core.NewEntries;
using NotesForYou.Core.Settings;
using Xamarin.Forms;

namespace NotesForYou.Core
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(EntriesPage), typeof(EntriesPage));
            Routing.RegisterRoute(nameof(NewEntryPage), typeof(NewEntryPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await NavigateTo(new LoginPage());
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await NavigateTo(new SettingsPage());
        }

        private async void OnNotesClicked(object sender, EventArgs e)
        {
            await NavigateTo(new EntriesPage());
        }

        private async Task NavigateTo(ContentPage page)
        {
            var currentType = CurrentPage.GetType();
            if (currentType == page?.GetType())
            {
                Current.FlyoutIsPresented = false;
                return;
            }
            await Navigation.PushAsync(page);
        }
    }
}
