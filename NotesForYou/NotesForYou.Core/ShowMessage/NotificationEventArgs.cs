using System;
using System.Collections.Generic;
using System.Text;

namespace NotesForYou.Core
{
    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
