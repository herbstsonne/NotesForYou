using System;
using NotesForYou.Core.AllEntries;
using NotesForYou.Core.NewEntries;
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
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//SettingsPage");
        }
    }
}
