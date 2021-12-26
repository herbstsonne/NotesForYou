using Xamarin.Forms;

namespace NotesForYou.Core.NewEntries
{
    public partial class NewEntryPage : ContentPage
    {
        public Note Note { get; set; }

        public NewEntryPage()
        {
            InitializeComponent();
            BindingContext = new NewNoteViewModel();
        }
    }
}