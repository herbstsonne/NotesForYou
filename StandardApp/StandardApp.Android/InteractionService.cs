﻿using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Widget;
using JournalToGo.Droid;
using StandardApp.Core;
using StandardApp.Core.AllEntries;

namespace StandardApp.Droid
{
    [Service]
    public class InteractionService : Service
    {
        public static RemoteViews widgetView;
        public static string ACTION_WIDGET_NEWENTRYSAVE = "Enter new entry";
        public static string ACTION_WIDGET_OPENAPP = "Open app";
        private AllEntriesDataAccessor _dataAccessor;

        public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
        {
            using (widgetView = BuildRemoteView (this)) {
                
                ComponentName thisWidget = new ComponentName (this, Java.Lang.Class.FromType (typeof (NewEntryWidget)).Name);
                AppWidgetManager manager = AppWidgetManager.GetInstance (this);
                manager.UpdateAppWidget (thisWidget, widgetView);
            }
            return StartCommandResult.Sticky;
        }

        private RemoteViews BuildRemoteView (Context context)
        {
            widgetView = new RemoteViews(context.PackageName, Resource.Layout.widget_newentry);

            ShowAppData();
            RegisterClicks(context);
            
            return widgetView;
        }

        private void ShowAppData()
        {
            var latestEntry = new AllEntriesDataAccessor(new JournalingContext()).GetLatestEntry();
            widgetView.SetTextViewText(Resource.Id.day, latestEntry.Day.ToShortDateString());
            widgetView.SetTextViewText(Resource.Id.blog_title, latestEntry.Headline);
        }

        private void RegisterClicks(Context context)
        {
            var activityIntent = new Intent (context, typeof (MainActivity));
            var configPendingIntent = PendingIntent.GetActivity (context, 0, activityIntent, 0);
            widgetView.SetOnClickPendingIntent (Resource.Id.buttonopenapp, configPendingIntent);
            
            var widgetIntent = new Intent(context, typeof(NewEntryWidget));
            widgetIntent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
            widgetView.SetOnClickPendingIntent(Resource.Id.newentrybuttonsave, GetPendingSelfIntent(context, ACTION_WIDGET_NEWENTRYSAVE));
        }
        
        private PendingIntent GetPendingSelfIntent(Context context, string action)
        {
            var intent = new Intent(context, typeof(NewEntryWidget));
            intent.SetAction(action);
            return PendingIntent.GetBroadcast(context, 0, intent, 0);
        }

        public override IBinder OnBind (Intent intent)
        {
            return null;
        }
    }
}