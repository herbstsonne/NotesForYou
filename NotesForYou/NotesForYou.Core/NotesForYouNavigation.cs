using NotesForYou.Core.AllEntries;
using NotesForYou.Core.Login;
using NotesForYou.Core.NewEntries;
using NotesForYou.Core.Settings;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NotesForYou.Core
{
    public class NotesForYouNavigation
    {
        public static void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(EntriesPage), typeof(EntriesPage));
            Routing.RegisterRoute(nameof(NewEntryPage), typeof(NewEntryPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }

        public static async Task NavigateTo(ContentPage page)
        {
            var currentType = Shell.Current?.CurrentPage?.GetType();
            if (currentType == page?.GetType())
            {
                Shell.Current.FlyoutIsPresented = false;
                return;
            }
            await Shell.Current?.Navigation?.PushAsync(page);
        }
    }
}
