using Android.App;
using Android.Content;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using NotesForYou.Core.AllEntries;
using NotesForYou.Core.Settings;
using NotesForYou.Core;

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
            SettingsNotifier.ShowNotificationInDefinedTimes = ShowNotificationInDefinedTimes;
            await SettingsNotifier.ShowNotificationInDefinedTimes.Invoke();
        }

        private async Task ShowNotificationInDefinedTimes()
        {
            await WaitForDefinedTime();
            ShowNotificationEvery24Hours();
        }

        private async Task WaitForDefinedTime()
        {
            try
            {
                ISettingsDataAccessor _settingsDataAccessor = (ISettingsDataAccessor)App.ServiceProvider.GetService(typeof(ISettingsDataAccessor));
                var showTime = await _settingsDataAccessor.GetShowTime();
                var timeSpan = NotificationTimeCalculator.CalculateInitialTimeSpanToShowTime(showTime);

                await Task.Delay(timeSpan);
            }
            catch (Exception e)
            {
                Console.WriteLine(nameof(NotificationIntentService) + ": " + e.Message);
            }
        }

        private void ShowNotificationEvery24Hours()
        {
            Console.WriteLine("Start new timer");
            Device.StartTimer(TimeSpan.FromSeconds(30), () =>
            {
                Task.Run(async () =>
                {
                    var dataRetriever = (INoteForwarder)App.ServiceProvider.GetService(typeof(INoteForwarder));
                    await dataRetriever.DisplayNotification();
                });
                return true;
            });
        }
    }
}