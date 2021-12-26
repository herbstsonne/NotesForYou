using Xamarin.Forms;

namespace StandardApp.Core.DailyEntries
{
    public partial class DailyEntryPage : ContentPage
    {
        public DailyEntryPage()
        {
            InitializeComponent();
            BindingContext = new DailyEntryViewModel();
        }
    }
}