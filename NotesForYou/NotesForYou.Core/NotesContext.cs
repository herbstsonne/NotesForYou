using System.IO;
using Microsoft.EntityFrameworkCore;
using Xamarin.Essentials;

namespace NotesForYou.Core
{
    public class NotesContext : DbContext
    {
        public DbSet<Note> Note { get; set; }

        public NotesContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
    }
}
