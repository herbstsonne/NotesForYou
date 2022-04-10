using Android.App;
using Android.Content;
using Android.OS;
using NotesForYou.Core;
using NotesForYou.Core.AllEntries;
using NotesForYou.Droid;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(startServiceAndroid))]
namespace NotesForYou.Droid
{
    [Service]
    public class startServiceAndroid : Service, IStartService
    {
        public void StartForegroundService()
        {
            var intent = new Intent(MainActivity.Instance.ApplicationContext, typeof(startServiceAndroid));
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                MainActivity.Instance.ApplicationContext.StartForegroundService(intent);
            }
            else
            {
                MainActivity.Instance.ApplicationContext.StartService(intent);
            }


        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            try
            {
                Device.StartTimer(TimeSpan.FromSeconds(15), () =>
                { 
                Intent notificationIntent = new Intent(this, typeof(NotificationIntentService));

                //Thread.Sleep(20000);

                var link = notificationIntent.GetStringExtra("Link");
                var headline = notificationIntent.GetStringExtra("Headline");
                var category = notificationIntent.GetStringExtra("Category");

                string messageBody = link ?? "not loaded";
                var notification = new Notification.Builder(this, "10111")
                .SetContentTitle($"{category}: {headline}")
                .SetContentText(messageBody)
                .SetSmallIcon(Resource.Drawable.icon_about)
                .SetOngoing(true)
                .Build();

                CreateNotificationChannel();

                StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);
                    return true;
                });
            }
            catch { 
            }
            return StartCommandResult.Sticky;
        }

        public override void OnCreate()
        {
            base.OnCreate(); 
            try
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {
                    var notification = new Notification.Builder(this, "10111")
                    .SetContentTitle("")
                    .SetContentText("")
                    .SetSmallIcon(Resource.Drawable.icon_about)
                    .SetOngoing(true)
                    .Build();
                    notification.Flags |= NotificationFlags.OngoingEvent;

                    CreateNotificationChannel();
                    StartForeground(1, notification);
                }
                
 
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }
            var channelName = Resources.GetString(Resource.String.channel_name);
            //var channelDescription = GetString(Resource.String.channel_description);
            var channel = new NotificationChannel("10111", channelName, NotificationImportance.Max)
            {
                Description = "123",//channelDescription
                LockscreenVisibility = NotificationVisibility.Public
            };
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}