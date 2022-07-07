using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesForYou.Core;

namespace NotesForYou.Test.Mocks
{
    public class MockDataStore : IDataStore<Note>
    {
        readonly List<Note> items;

        public MockDataStore()
        {
            items = new List<Note>()
            {
                new Note { Id = 1, Date = new DateTime(2021, 5, 1), Headline = "Glückstag", Link="http://hi" },
                new Note { Id = 2, Date = new DateTime(2021, 5, 2), Headline = "Sonnenschein", Link="This is an item description." },
                new Note { Id = 3, Date = new DateTime(2021, 5, 3), Headline = "Freunde", Link="This is an item description." },
                new Note { Id = 4, Date = new DateTime(2021, 5, 4), Headline = "Relaxed", Link="This is an item description." },
                new Note { Id = 5, Date = new DateTime(2021, 5, 5), Headline = "Urlaub", Link="This is an item description." },
                new Note { Id = 6, Date = new DateTime(2021, 5, 6), Headline = "Happy", Link="This is an item description." }
            };
        }

        public async Task<bool> AddEntryAsync(Note item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateEntryAsync(Note item)
        {
            var oldItem = items.Where((Note arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteEntryAsync(int id)
        {
            var oldItem = items.Where((Note arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Note> GetEntryAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Note>> GetEntriesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}