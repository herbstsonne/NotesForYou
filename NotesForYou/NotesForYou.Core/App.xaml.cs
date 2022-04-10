using Microsoft.Extensions.DependencyInjection;
using NotesForYou.Core.AllEntries;
using System;
using Xamarin.Forms;

namespace NotesForYou.Core
{
    public partial class App : Application
    {
        protected static IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            InitializeComponent();

            SetupServices();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        void SetupServices()
        {
            var services = new ServiceCollection();

            var dbContext = new NotesContext();
            var dataAccessor = new AllNoteEntriesDataAccessor(dbContext);
            var contentRetriever = new NotificationContentRetriever()
            {
                DataAccessor = dataAccessor
            };

            DependencyService.RegisterSingleton(dbContext);
            DependencyService.RegisterSingleton<INotificationContentRetriever>(contentRetriever);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
