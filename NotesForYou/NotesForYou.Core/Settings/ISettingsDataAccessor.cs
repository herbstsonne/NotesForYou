using System;
using System.Threading.Tasks;

namespace NotesForYou.Core.Settings
{
    public interface ISettingsDataAccessor
    {
        Task Save(Setting setting);
        Task<TimeSpan> GetShowTime();
    }
}
