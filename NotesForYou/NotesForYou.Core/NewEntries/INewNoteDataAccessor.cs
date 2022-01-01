namespace NotesForYou.Core.NewEntries
{
    public interface INewNoteDataAccessor
    {
        bool Validate(string headline, string dailyThoughtsText, Category _category);
        void Save(Note entry);
    }
}