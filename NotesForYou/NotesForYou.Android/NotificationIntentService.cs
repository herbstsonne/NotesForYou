using Android.App;
using Android.Content;
using NotesForYou.Core;
using System;
using System.Threading.Tasks;
using NotesForYou.Core.Database;
using NotesForYou.Core.ShowMessage;
using Xamarin.Forms;

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

            Device.StartTimer(TimeSpan.FromSeconds(30), () =>
            {
                Task.Run(async () =>
                {
                    Note note = null;
                    var dataRetriever = DependencyService.Resolve<INoteForwarder>();
                    await dataRetriever.DisplayNotification();
                });
                return true;
            });
        }
    }
}