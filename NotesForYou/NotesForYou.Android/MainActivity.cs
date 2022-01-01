using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using NotesForYou.Core;
using Android.Content;
using JournalToGo.Droid;
using Java.Interop;
using Android.Views;
using NotesForYou.Android;
using Android.Widget;
using System;
using Xamarin.Forms;

namespace NotesForYou.Droid
{
    [Activity(Label = "NotesForYou", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);

            SQLitePCL.Batteries_V2.Init();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());


            StartService(new Intent(this, typeof(AppService)));

            //DependencyService.Get<INotificationManager>().SendNotification("test", "hi");
            //WakefulIntentService.SendWakefulWork(this, typeof(AppService));

            //Intent i = new Intent(this, typeof(AlarmReceiver)).PutExtra("1", true);
            //PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0, i, PendingIntentFlags.UpdateCurrent);
            //AlarmManager manager = (AlarmManager)GetSystemService(Application.AlarmService);

            //manager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + 1000, pendingIntent);
        }
        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}