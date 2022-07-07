namespace NotesForYou.Core.Extension
{
    public static class MessageExtension
    {
        public static string GetNoteNotAvailableText(this Note note)
        {
            return "No more new notes available.Add new ones :)";
        }
    }
}
