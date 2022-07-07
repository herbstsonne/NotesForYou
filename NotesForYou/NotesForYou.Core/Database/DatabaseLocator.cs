using System;
using System.IO;
using Xamarin.Forms;

namespace NotesForYou.Core.Database
{
    public static class DatabaseLocator
    {
        private const string databaseName = "notes.db3";

        public static string RetrieveDb()
        {
            string databasePath;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName); ;
                    break;
                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
                    break;
                default:
                    throw new NotImplementedException("Platform not supported");
            }
            return databasePath;
        }
    }
}
