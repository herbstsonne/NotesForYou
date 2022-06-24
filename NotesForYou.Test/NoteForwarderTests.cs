using Moq;
using NotesForYou.Core.AllEntries;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;
using NotesForYou.Core.ShowMessage;

namespace NotesForYou.Test
{
    public class NoteForwarderTests
    {
        private INoteForwarder _sut;
        private Mock<IAllNoteEntriesDataAccessor> _mockDataAccessor;

        [SetUp]
        public void Setup()
        {
            _mockDataAccessor = new Mock<IAllNoteEntriesDataAccessor>();
            _sut = new NoteForwarder(_mockDataAccessor.Object);
        }

        [Test]
        public async Task WhenGetUpdatedNote_WithValidNote_UpdateNote()
        {
            var note = new Note();
            _mockDataAccessor.Setup(x => x.GetRandomNote()).Returns(Task.FromResult(note));
            _mockDataAccessor.Setup(x => x.UpdateNote(note));

            var updatedNote = await _sut.GetUpdatedNote();
            note.Should().Be(updatedNote);
        }

        [Test]
        public async Task WhenGetUpdatedNote_WithNotValidNote_NoteIsNull()
        {
            Note note = null;
            _mockDataAccessor.Setup(x => x.GetRandomNote()).Returns(Task.FromResult(note));

            var updatedNote = await _sut.GetUpdatedNote();
            updatedNote.Should().BeNull();
        }
    }
}
