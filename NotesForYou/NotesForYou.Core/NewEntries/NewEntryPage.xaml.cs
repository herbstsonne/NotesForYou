using System;
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

        private void Picker_SelectedIndexChanged(object o, EventArgs e)
        {
            var picker = (Picker)o;
            var context = (NewNoteViewModel)BindingContext;
            if (picker.SelectedIndex == -1)
                return;
            context.SelectedCategory = (Category)picker.SelectedIndex;
        }
    }
}