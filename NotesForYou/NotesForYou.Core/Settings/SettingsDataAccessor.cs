using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesForYou.Core.Database;

namespace NotesForYou.Core.Settings
{
    public class SettingsDataAccessor : ISettingsDataAccessor
    {
        private NotesContext _noteContext;
        private Setting _setting;

        public SettingsDataAccessor(NotesContext noteContext)
        {
            this._noteContext = noteContext;
        }

        public async Task Save(Setting setting)
        {
            _setting = setting;
            try
            {
                _noteContext.Setting.Update(setting);
                await _noteContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<TimeSpan> GetShowTime()
        {
            if (_setting == null)
            {
                var settings = await _noteContext.Setting.ToListAsync();
                _setting = settings.Find(s => s.Id == 0);
                if (_setting == null)
                    return DateTime.Now.TimeOfDay.Add(TimeSpan.FromSeconds(30));
            }
            return _setting.ShowTime;
        }
    }
}
