using System;

namespace NotesForYou.Core.ShowNote
{
    public static class NotificationTimeCalculator
    {
        public static TimeSpan CalculateInitialTimeSpanToShowTime(TimeSpan showTime)
        {
            var dateTime = DateTime.Today.Add(showTime);
            Console.WriteLine("Selected time: " + showTime);
            var difference = dateTime.Subtract(DateTime.Now);
            if (difference.TotalMilliseconds < 0)
            {
                difference = dateTime.AddDays(1).Subtract(DateTime.Now);
            }
            return difference;
        }
    }
}
