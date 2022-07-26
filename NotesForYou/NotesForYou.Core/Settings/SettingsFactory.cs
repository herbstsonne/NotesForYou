using System;

namespace NotesForYou.Core.Settings
{
    public class SettingsFactory
    {
        public static Setting Create(TimeSpan showTime)
        {
            return new Setting
            {
                ShowTime = showTime
            };
        }
    }
}
