using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Android.Provider;
using Android;
using NotesForYou.Core;

namespace JournalToGo.Droid
{
    [Service]
    [Obsolete]
    public class AppService : WakefulIntentService
    {
        private int NOTIFY_ID = 1337;
        public AppService() : base("AppService")
        {

        }

        protected override void DoWakefulWork(Intent intent)
        {
            Toast.MakeText(this, "In service", ToastLength.Short).Show();
            Log.Info("AppService", "I'm awake! I'm awake!");

            //get settings
            //set alarm
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                DependencyService.Get<INotificationManager>().SendNotification("test", "hi");

                return true; // return true to repeat counting, false to stop timer
            });
        }

        [Obsolete]
        private Notification.Builder BuildNormal(Context context)
        {
            Notification.Builder builder = new Notification.Builder(context);

            builder.SetAutoCancel(true)
                .SetContentTitle("Complete")
                .SetContentText("Fun")
                .SetContentIntent(BuildPendingIntent(context, Settings.ActionSecuritySettings))
                .SetSmallIcon(Resource.Drawable.AlertDarkFrame)
                .SetTicker("Complete")
                .AddAction(Resource.Id.Icon, "Play", BuildPendingIntent(context, Settings.ActionSettings));

            return builder;
        }
        private PendingIntent BuildPendingIntent(Context context, string action)
        {
            Intent i = new Intent(action);
            return (PendingIntent.GetActivity(context, 0, i, 0));
        }
    }
}