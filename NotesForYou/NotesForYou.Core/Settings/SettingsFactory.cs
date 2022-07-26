using System;

namespace NotesForYou.Core.Settings
{
    public class SettingsFactory
    {
        public static Setting Create(TimeSpan difference, TimeSpan showTime)
        {
            return new Setting
            {
                Difference = difference,
                ShowTime = showTime
            };
        }
    }
}
