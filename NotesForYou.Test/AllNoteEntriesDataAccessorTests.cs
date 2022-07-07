using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NotesForYou.Core;
using NotesForYou.Core.Database;
using NotesForYou.Core.Notes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesForYou.Test
{
    public class AllNoteEntriesDataAccessorTests
    {
        private INotesDataAccessor _sut;
        private Mock<NotesContext> _mockContext;
        private IList<Note> entities;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<NotesContext>();
            _sut = new NotesDataAccessor(_mockContext.Object);

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
            entities = new List<Note> { note1, note2, note3, note2 };
            _mockContext.Setup(x => x.Note).ReturnsDbSet(entities);
        }

        [Test]
        public async Task WhenGetAllAndNotesAvailable_ThenGetAllNotesSortedByDate()
        {
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
            var note5 = new Note
            {
              
            };
            entities.Add(note5);
            _mockContext.Setup(x => x.Note).ReturnsDbSet(entities);
            entities.Count.Should().Be(5);

            var res = await _sut.GetAll();

            res.Should().NotBeNull();
            res.Count.Should().Be(4);
            res[0].Date.Should().NotBeNull();
            res[1].Date.Should().NotBeNull();
            res[2].Date.Should().NotBeNull();
            res[3].Date.Should().NotBeNull();
        }

        [Test]
        public async Task WhenGetRandomNotes_ThenGetOneRandomNote()
        {
            var note5 = new Note
            {

            };
            entities.Add(note5);
            _mockContext.Setup(x => x.Note).ReturnsDbSet(entities);
            var res = await _sut.GetRandomNote();

            res.Should().NotBeNull();
            res.Date.Should().BeNull();
            entities.Should().Contain(res);
        }

        [Test]
        public void WhenUpdateNote_ThenSetCurrentDate()
        {
            var note = new Note { };
            entities.Add(note);
            _mockContext.Setup(x => x.Note).ReturnsDbSet(entities);

            _sut.UpdateNote(note);

            note.Should().NotBeNull();
            note.Date.Should().NotBeNull();
            entities.Should().Contain(note);
        }
    }
}
