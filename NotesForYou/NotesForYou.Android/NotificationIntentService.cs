using Android.App;
using Android.Content;
using Android.Util;
using NotesForYou.Core;
using System;
using System.Threading.Tasks;
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
            string servicename = typeof(NotificationIntentService).Name;
            Log.Info(servicename, "Starting background work: load random note");

            Device.StartTimer(TimeSpan.FromSeconds(30), () =>
            {
                Task.Run(async () =>
                {
                    Note note = null;
                    var dataRetriever = DependencyService.Resolve<INotificationContentRetriever>();
                    try
                    {
                        note = await dataRetriever.SelectNote();
                        if (note == null)
                        {
                            Console.WriteLine("No more new notes available." +
                                "Add new ones :)");
                            DependencyService.Get<INotificationManager>().SendNotification("No more new notes available." +
                                "Add new ones :)", "");
                        }
                        else
                        {
                            DependencyService.Get<INotificationManager>().SendNotification(note.Headline, note.Link);
                            intent.PutExtra("Link", note.Link);
                            intent.PutExtra("Headline", note.Headline);
                            intent.PutExtra("Category", note.Category);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });
                return true;
            });
        }
    }
}