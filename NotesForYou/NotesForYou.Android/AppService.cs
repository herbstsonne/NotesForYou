using Android.App;
using Android.Content;
using Android.Util;
using Android.Widget;
using System;
using Xamarin.Forms;
using NotesForYou.Core;
using NotesForYou.Core.AllEntries;
using System.Threading.Tasks;
using Android;
using AndroidX.Core.App;

namespace JournalToGo.Droid
{
    [Service]
    [Obsolete]
    public class AppService : WakefulIntentService
    {
        const int NOTIFICATION_ID = 9000;

        public AppService() : base("AppService")
        {
        }


        protected override void DoWakefulWork(Intent intent)
        {
            Toast.MakeText(this, "In service", ToastLength.Short).Show();
            Log.Info("AppService", "I'm awake! I'm awake!");

            //get settings
            //set alarm
//            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
//            {
//                Task.Run(async () =>
//                {
//                    var dbContext = new NotesContext();
//                    DependencyService.RegisterSingleton(dbContext);
//                    using (var _allEntriesDataAccessor = new AllNoteEntriesDataAccessor(dbContext))
//                    {
//                        try
//                        {
//                            var note = await _allEntriesDataAccessor.GetRandomNote();
//                            if (note == null)
//                            {
//                                Toast.MakeText(this, "No more new notes available. Add new ones :)", ToastLength.Short).Show();
//                                return;
//                            }

//                            DependencyService.Get<INotificationManager>().SendNotification(note.Headline, note.Link);
//                            _allEntriesDataAccessor.UpdateNote(note);
//                        }
//                        catch (Exception e)
//                        {
//#if DEBUG
//                            Console.WriteLine(e.Message);
//#endif
//                            Toast.MakeText(this, $"Exception occurred: {e.Message}", ToastLength.Short).Show();
//                        }
//                    };
//                });
//                return true; // return true to repeat counting, false to stop timer
//            });
        }
    }
}