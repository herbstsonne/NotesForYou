using Xamarin.Forms;

namespace StandardApp.Core.NewEntries
{
    public partial class NewEntryPage : ContentPage
    {
        public JournalEntry Entry { get; set; }

        public NewEntryPage()
        {
            InitializeComponent();
            BindingContext = new NewEntryViewModel();
        }
    }
}