using System;
using NotesForYou.Core.AllEntries;
using NotesForYou.Core.Login;
using NotesForYou.Core.Settings;
using Xamarin.Forms;

namespace NotesForYou.Core
{
    public partial class AppShell : Shell
    {
        public AppShellViewModel ViewModel { get; set; }

        public AppShell()
        {
            InitializeComponent();
            BindingContext = ViewModel = new AppShellViewModel();

            NotesForYouNavigation.RegisterRoutes();
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await NotesForYouNavigation.NavigateTo(new LoginPage());
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await NotesForYouNavigation.NavigateTo(new SettingsPage());
        }

        private async void OnNotesClicked(object sender, EventArgs e)
        {
            await NotesForYouNavigation.NavigateTo(new EntriesPage());
        }
    }
}
