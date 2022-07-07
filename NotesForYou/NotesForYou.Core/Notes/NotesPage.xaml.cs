using System;
using Xamarin.Forms;

namespace NotesForYou.Core.Notes
{
    public partial class NotesPage : ContentPage
    {
        NotesViewModel _viewModel;

        public NotesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NotesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void ToolbarItem_OnClicked(object sender, EventArgs e)
        {
            App.Current.UserAppTheme = 
                App.Current.UserAppTheme == OSAppTheme.Light || App.Current.UserAppTheme == OSAppTheme.Unspecified ? 
                OSAppTheme.Dark : OSAppTheme.Light;
        }
    }
}