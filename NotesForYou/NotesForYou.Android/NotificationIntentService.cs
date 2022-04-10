using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using NotesForYou.Core;
using NotesForYou.Core.AllEntries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            Device.StartTimer(TimeSpan.FromSeconds(15), () =>
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
                            Console.WriteLine("No more new notes available.Add new ones :)");
                        }
                        else
                        {
                            DependencyService.Get<INotificationManager>().SendNotification(note.Headline, note.Link);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    if (note != null)
                    {
                        intent.PutExtra("Link", note.Link);
                        intent.PutExtra("Headline", note.Headline);
                        intent.PutExtra("Category", note.Category);
                    }
                });
                return true;
            });
        }
    }
}