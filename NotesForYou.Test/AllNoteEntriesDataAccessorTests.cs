using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NotesForYou.Core.AllEntries;
using NotesForYou.Core.Database;
using NotesForYou.Core.ShowMessage;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesForYou.Test
{
    public class AllNoteEntriesDataAccessorTests
    {
        private IAllNoteEntriesDataAccessor _sut;
        private Mock<NotesContext> _mockContext;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<NotesContext>();
            _sut = new AllNoteEntriesDataAccessor(_mockContext.Object); 
        }

        [Test]
        public async Task WhenGetAllAndNotesAvailable_ThenGetAllNotesSortedByDate()
        {
            var note1 = new Note
            {
                Date = DateTime.Now.AddDays(1)
            };
            var note2 = new Note
            {
                Date = DateTime.Now.AddDays(-1)
            };
            var note3 = new Note
            {
                Date = DateTime.Now
            };
            IList<Note> entities = new List<Note> { note1, note2, note3, note2 };
            _mockContext.Setup(x => x.Note).ReturnsDbSet(entities);

            var res = await _sut.GetAll();

            res.Should().NotBeNull();
            res[0].Date.Should().NotBeNull();
            res[0].Date.Should().BeSameDateAs((DateTime)res[1].Date);
            res[1].Date.Should().BeBefore((DateTime)res[2].Date);
            res[2].Date.Should().BeBefore((DateTime)res[3].Date);
        }

        [Test]
        public async Task WhenGetAllAndNotesAvailable_ThenGetAllNotesWithDateNotNull()
        {
            var note1 = new Note
            {
                Date = DateTime.Now.AddDays(1)
            };
            var note2 = new Note
            {
              
            };
            var note3 = new Note
            {
                Date = DateTime.Now
            };
            IList<Note> entities = new List<Note> { note1, note2, note3, note2 };
            _mockContext.Setup(x => x.Note).ReturnsDbSet(entities);

            var res = await _sut.GetAll();

            res.Should().NotBeNull();
            res.Count.Should().Be(2);
            res[0].Date.Should().NotBeNull();
            res[1].Date.Should().NotBeNull();
            res[0].Date.Should().BeBefore((DateTime)res[1].Date);
        }
    }
}
