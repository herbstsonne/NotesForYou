using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using NotesForYou.Core.Database;

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
                this.Database.EnsureCreated();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databasePath = DatabaseLocator.RetrieveDb();

            if (!File.Exists(databasePath))
            {
                File.Create(databasePath);
            }
            optionsBuilder
                .UseSqlite($"Filename={databasePath}");
        }
    }
}
