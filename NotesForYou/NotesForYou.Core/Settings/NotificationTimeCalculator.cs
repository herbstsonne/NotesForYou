using System;

namespace NotesForYou.Core.Settings
{
    public static class NotificationTimeCalculator
    {
        public static TimeSpan CalculateInitialTimeSpanToShowTime(DateTime showTime)
        {
            var difference = showTime.TimeOfDay.Subtract(DateTime.Now.TimeOfDay);
            if (difference.TotalMilliseconds < 0)
                difference = showTime.AddHours(24).TimeOfDay.Subtract(DateTime.Now.TimeOfDay);
            return difference;
        }
    }
}
