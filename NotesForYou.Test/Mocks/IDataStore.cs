using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StandardApp.Test.Mocks
{
    public interface IDataStore<T>
    {
        Task<bool> AddEntryAsync(T item);
        Task<bool> UpdateEntryAsync(T item);
        Task<bool> DeleteEntryAsync(int id);
        Task<T> GetEntryAsync(int id);
        Task<IEnumerable<T>> GetEntriesAsync(bool forceRefresh = false);
    }
}
