using System;
using System.Threading.Tasks;

namespace NotesForYou.Core.ShowNote
{
    public static class NotificationNotifier
    {
        public static Func<Task> ShowNotificationInDefinedTimes { get; set; }
    }
}
