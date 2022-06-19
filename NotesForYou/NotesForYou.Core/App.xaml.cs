using Microsoft.Extensions.DependencyInjection;
using NotesForYou.Core.AllEntries;
using NotesForYou.Core.Settings;
using System;
using NotesForYou.Core.Database;
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

        private void SetupServices()
        {
            var services = new ServiceCollection();

            var dbContext = new NotesContext();
            DependencyService.RegisterSingleton(dbContext);

            var dataAccessor = new AllNoteEntriesDataAccessor(dbContext);
            var contentRetriever = new NoteForwarder(dataAccessor);
            var settingsAccessor = new SettingsDataAccessor(dbContext);

            DependencyService.RegisterSingleton<INoteForwarder>(contentRetriever);
            DependencyService.RegisterSingleton(settingsAccessor);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
