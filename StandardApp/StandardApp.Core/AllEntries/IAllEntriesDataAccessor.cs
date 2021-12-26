using System.Collections.Generic;
using System.Threading.Tasks;

namespace StandardApp.Core.AllEntries
{
    public interface IAllEntriesDataAccessor
    {
        List<JournalEntry> GetAllEntries(List<JournalEntry> entries);
        JournalEntry GetLatestEntry();
    }
}