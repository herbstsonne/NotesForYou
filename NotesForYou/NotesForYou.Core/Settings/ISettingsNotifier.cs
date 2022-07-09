using System;
using System.Threading.Tasks;

namespace NotesForYou.Core.Settings
{
    public interface ISettingsNotifier
    {
        Func<Task> ShowNotificationInDefinedTimes { get; set; }
    }
}
