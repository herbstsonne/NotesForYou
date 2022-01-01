using Android.App;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Text;

namespace JournalToGo.Droid
{
    [Service]
    [Obsolete]
    public class DemoIntentService : IntentService
    {
        [Obsolete]
        public DemoIntentService() : base("DemoIntentService")
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle("Confirm delete");
            alert.SetMessage("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
            alert.SetPositiveButton("OK", (senderAlert, args) => {
                Toast.MakeText(this, "Ok button Tapped!", ToastLength.Short).Show();
            });

            Dialog dialog = alert.Create();
            dialog.Window.SetType(Android.Views.WindowManagerTypes.Toast);
            dialog.Show();
        }

        protected override void OnHandleIntent(Android.Content.Intent intent)
        {
            Console.WriteLine("send note at a certain time");
            //get time out of settings

            Console.WriteLine("work complete");
        }
    }
}
