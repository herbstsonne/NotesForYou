namespace NotesForYou.Core.AddNote
{
    public interface IAddNoteDataAccessor
    {
        bool Validate(string headline, string dailyThoughtsText, Category _category);
        void Save(Note entry);
    }
}