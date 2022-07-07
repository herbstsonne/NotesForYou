using System;
using Microsoft.EntityFrameworkCore;
using NotesForYou.Core.Settings;

namespace NotesForYou.Core.Database
{
    public class NotesContext : DbContext
    {
        public virtual DbSet<Note> Note { get; set; }
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

            optionsBuilder
                .UseSqlite($"Filename={databasePath}");
        }
    }
}
