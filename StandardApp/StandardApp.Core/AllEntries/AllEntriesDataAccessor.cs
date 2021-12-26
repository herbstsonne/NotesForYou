using System;
using System.Collections.Generic;
using System.Linq;

namespace StandardApp.Core.AllEntries
{
    public class AllEntriesDataAccessor : IAllEntriesDataAccessor
    {
        private readonly JournalingContext _context;

        public AllEntriesDataAccessor(JournalingContext context)
        {
            _context = context;
        }
        public List<JournalEntry> GetAllEntries(List<JournalEntry> entries)
        {
            var items = _context.JournalEntry.ToList(); 
            items.Sort((a, b) => DateTime.Compare(a.Day, b.Day));

            foreach (var item in items)
            {
                entries.Add(item);
            }

            return entries;
        }

        public JournalEntry GetLatestEntry()
        {
            var allEntries = GetAllEntries(new List<JournalEntry>());
            return allEntries.FirstOrDefault();
        }
    }
}