using System;
using Xamarin.Forms;

namespace NotesForYou.Core.AddNote
{
    public partial class AddNotePage : ContentPage
    {
        public Note Note { get; set; }

        public AddNotePage()
        {
            InitializeComponent();
            BindingContext = new AddNoteViewModel();
        }

        private void Picker_SelectedIndexChanged(object o, EventArgs e)
        {
            var picker = (Picker)o;
            var context = (AddNoteViewModel)BindingContext;
            if (picker.SelectedIndex == -1)
                return;
            context.SelectedCategory = (Category)picker.SelectedIndex;
        }
    }
}