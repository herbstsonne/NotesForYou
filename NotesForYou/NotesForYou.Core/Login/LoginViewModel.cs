﻿using NotesForYou.Core.AllEntries;
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
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(EntriesPage)}");
        }
    }
}
