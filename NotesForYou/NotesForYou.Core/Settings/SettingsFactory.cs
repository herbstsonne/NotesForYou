using System;

namespace NotesForYou.Core.ShowTime
{
    public class SettingsFactory
    {
        public static Setting Create(TimeSpan showTime)
        {
            return new Setting
            {
                Id = Guid.NewGuid(),
                ShowTime = showTime
            };
        }
    }
}
