using System;
using System.Threading.Tasks;

namespace NotesForYou.Core.Settings
{
    public class SettingsDataAccessor
    {
        private NotesContext _noteContext;

        public SettingsDataAccessor(NotesContext noteContext)
        {
            this._noteContext = noteContext;
        }

        public async Task Save(Setting setting)
        {
            try
            {
                await _noteContext.Setting.AddAsync(setting);
                await _noteContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
