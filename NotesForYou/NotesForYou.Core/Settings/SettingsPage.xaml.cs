using NotesForYou.Core.Notes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesForYou.Core.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = new SettingsViewModel();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Task.Run(async () =>
            {
                await NotesForYouNavigation.NavigateTo(new NotesPage());
            });
        }
    }
}