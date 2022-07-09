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
        private readonly ServiceCollection _serviceCollection;

        public static IServiceProvider ServiceProvider { get; set; }

        public App(ServiceCollection serviceCollection)
        {
            InitializeComponent();

            this._serviceCollection = serviceCollection;
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
            var dbContext = new NotesContext();
            //DependencyService.RegisterSingleton(dbContext);

            var dataAccessor = new NotesDataAccessor(dbContext);
            var contentRetriever = new NoteForwarder(dataAccessor);
            var settingsAccessor = new SettingsDataAccessor(dbContext);

            //DependencyService.RegisterSingleton<INoteForwarder>(contentRetriever);
            //DependencyService.RegisterSingleton<ISettingsDataAccessor>(settingsAccessor);

            _serviceCollection.AddSingleton<NotesContext>(dbContext);
            _serviceCollection.AddSingleton<INoteForwarder>(contentRetriever);
            _serviceCollection.AddSingleton<ISettingsDataAccessor>(settingsAccessor);

            ServiceProvider = _serviceCollection.BuildServiceProvider();
        }
    }
}
