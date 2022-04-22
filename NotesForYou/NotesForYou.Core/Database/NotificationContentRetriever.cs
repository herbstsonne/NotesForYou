﻿using System;
using System.Threading.Tasks;
using NotesForYou.Core.AllEntries;
using NotesForYou.Core.ShowMessage;

namespace NotesForYou.Core.Database
{
    public class NotificationContentRetriever : INotificationContentRetriever
    {
        public AllNoteEntriesDataAccessor DataAccessor { get; set; }

        public NotificationContentRetriever()
        {
        }
        public async Task<Note> SelectNote()
        {
            try
            {
                var note = await DataAccessor.GetRandomNote();
                if (note == null)
                {
                    Console.WriteLine("No more new notes available.Add new ones :)");
                    return null;
                }

                DataAccessor.UpdateNote(note);
                return note;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
