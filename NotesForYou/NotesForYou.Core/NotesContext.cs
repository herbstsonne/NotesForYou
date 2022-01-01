using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Xamarin.Essentials;

namespace NotesForYou.Core
{
    public class NotesContext : DbContext
    {
        public DbSet<Note> Note { get; set; }
        public DbSet<Setting> Setting { get; set; }

        public NotesContext()
        {
            try
            {
                this.Database.Migrate();
                this.Database.EnsureCreated();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
    }
}
