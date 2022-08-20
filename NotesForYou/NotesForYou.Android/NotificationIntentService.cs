using Android.App;
using Android.Content;
using System;
using System.Threading.Tasks;
using NotesForYou.Core.AllEntries;
using NotesForYou.Core.Settings;
using NotesForYou.Core;
using System.Threading;

namespace NotesForYou.Droid
{
    [Service]
    public class NotificationIntentService : IntentService
    {
        public NotificationIntentService() : base("NotificationIntentService")
        {
        }

        protected override async void OnHandleIntent(Intent intent)
        {
            Console.WriteLine("Starting background work: load random note");
            SettingsNotifier.ResetEvent = new AutoResetEvent(false);
            await ShowNotificationInDefinedTimes();
        }

        private async Task ShowNotificationInDefinedTimes()
        {
            var waitingTime = await WaitForDefinedTime();
            ShowNotificationEvery24Hours(waitingTime);
        }

        private async Task<TimeSpan> WaitForDefinedTime()
        {
            TimeSpan waitingTime = new TimeSpan();
            try
            {
                ISettingsDataAccessor _settingsDataAccessor = (ISettingsDataAccessor)App.ServiceProvider.GetService(typeof(ISettingsDataAccessor));
                var showTime = await _settingsDataAccessor.GetShowTime();
                waitingTime = NotificationTimeCalculator.CalculateInitialTimeSpanToShowTime(showTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(nameof(NotificationIntentService) + ": " + e.Message);
            }
            return waitingTime;
        }

        private async void ShowNotificationEvery24Hours(TimeSpan waitingTime)
        {
            Console.WriteLine($"Start new timer at: {DateTime.Now}");
            var timer = new Timer(ShowFirstNote, SettingsNotifier.ResetEvent, waitingTime, TimeSpan.FromDays(1));
            
            while(true)
            {
                SettingsNotifier.ResetEvent.WaitOne();
                waitingTime = await WaitForDefinedTime();

                timer.Change(waitingTime, TimeSpan.FromDays(1));
            };
        }

        private void ShowFirstNote(Object stateInfo)
        {
            var noteForwarder = (INoteForwarder)App.ServiceProvider.GetService(typeof(INoteForwarder));
            Task.Run(async () =>
            {
                await noteForwarder.DisplayNotification();
            }
            );
        }
    }
}