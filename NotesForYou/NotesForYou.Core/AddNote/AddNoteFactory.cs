namespace NotesForYou.Core.AddNote
{
    public static class AddNoteFactory
    {
        public static Note Create(int category, string headline, string dailyThoughtsText)
        {
            return new Note()
            {
                Headline = headline,
                Link = dailyThoughtsText,
                Category = category
            };
        }
    }
}