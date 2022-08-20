using System;
using System.Threading;
using System.Threading.Tasks;

namespace NotesForYou.Core.Settings
{
    public static class SettingsNotifier
    {
        public static Func<Task> ShowNotificationInDefinedTimes { get; set; }
        public static AutoResetEvent ResetEvent { get; set; }
    }
}
