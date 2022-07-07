using NotesForYou.Core.Notes;
using Xamarin.Forms;

namespace NotesForYou.Core.Login
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            await NotesForYouNavigation.NavigateTo(new NotesPage());
        }
    }
}
