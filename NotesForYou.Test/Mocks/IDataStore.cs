using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StandardApp.Test.Mocks
{
    public interface IDataStore<T>
    {
        Task<bool> AddEntryAsync(T item);
        Task<bool> UpdateEntryAsync(T item);
        Task<bool> DeleteEntryAsync(Guid id);
        Task<T> GetEntryAsync(Guid id);
        Task<IEnumerable<T>> GetEntriesAsync(bool forceRefresh = false);
    }
}
