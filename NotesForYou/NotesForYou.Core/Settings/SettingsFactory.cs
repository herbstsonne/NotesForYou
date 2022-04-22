using System;

namespace NotesForYou.Core.ShowTime
{
    public class SettingsFactory
    {
        public static Setting Create(TimeSpan difference, DateTime minDate, DateTime maxDate)
        {
            return new Setting
            {
                Difference = difference,
                MinDate = minDate,
                MaxDate = maxDate
            };
        }
    }
}
