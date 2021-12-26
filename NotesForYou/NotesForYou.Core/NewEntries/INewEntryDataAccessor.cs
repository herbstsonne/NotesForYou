namespace NotesForYou.Core.NewEntries
{
    public interface INewEntryDataAccessor
    {
        bool Validate(string headline, string dailyThoughtsText);
        void Save(Note entry);
    }
}