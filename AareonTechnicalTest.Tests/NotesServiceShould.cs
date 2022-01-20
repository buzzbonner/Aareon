using AareonTechnicalTest.Models;
using AareonTechnicalTest.Repositories;
using AareonTechnicalTest.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Tests
{
    [TestClass]
    public class NotesServiceShould
    {
        private INoteRepository? _noteRepository;
        private NoteService? _noteService;
        private IPersonRepository? _personRepository;

        [TestInitialize]
        public void Initialise()
        {
            _noteRepository = Substitute.For<INoteRepository>();
            _personRepository = Substitute.For<IPersonRepository>();

            _noteService = new NoteService(_noteRepository, _personRepository);
        }

        [TestMethod]
        public async Task FailDeleteNoteWhenNoteNotFound()
        {
            var personId = 1;
            var noteId = 1;

            Note? noteMock = null;
            _noteRepository!.FindNoteAsync(noteId).Returns(noteMock);

            var response = await _noteService!.DeleteNoteAsync(personId, noteId);
            var x = response
                .Should()
                .BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task FailDeleteNoteWhenPersonNotFound()
        {
            var personId = 1;
            var noteId = 1;

            Note? noteMock = new Note { Id = noteId };
            _noteRepository!.FindNoteAsync(noteId).Returns(noteMock);

            Person? personMock = null;
            _personRepository!.FindPersonAsync(noteId).Returns(personMock);

            var response = await _noteService!.DeleteNoteAsync(personId, noteId);
            var x = response
                .Should()
                .BeOfType<UnprocessableEntityObjectResult>();
        }

        [TestMethod]
        public async Task FailDeleteNoteWhenPersonIsNotAdmin()
        {
            var personId = 1;
            var noteId = 1;

            Note? noteMock = new Note { Id = noteId };
            _noteRepository!.FindNoteAsync(noteId).Returns(noteMock);

            Person? personMock = new Person { Id = personId, IsAdmin = false };
            _personRepository!.FindPersonAsync(noteId).Returns(personMock);

            var response = await _noteService!.DeleteNoteAsync(personId, noteId);
            var x = response
                .Should()
                .BeOfType<UnprocessableEntityObjectResult>();

            var problemDetails = x.Subject.Value.Should().BeOfType<ProblemDetails>().Which;

            problemDetails.Detail.Should().Be($"The person with ID {personId} is not an administrator.", because: "The person was not an administrator");
        }


        [TestMethod]
        public async Task DeleteNoteWhenPersonIsAdmin()
        {
            var personId = 1;
            var noteId = 1;

            Note? noteMock = new Note { Id = noteId };
            _noteRepository!.FindNoteAsync(noteId).Returns(noteMock);

            Person? personMock = new Person { Id = personId, IsAdmin = true };
            _personRepository!.FindPersonAsync(noteId).Returns(personMock);

            var response = await _noteService!.DeleteNoteAsync(personId, noteId);
            var x = response
                .Should()
                .BeOfType<NoContentResult>();
        }
    }
}