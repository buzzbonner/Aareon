using AareonTechnicalTest.Models;
using AareonTechnicalTest.Repositories;
using Audit.Core;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IPersonRepository _personRepository;

        public NoteService(INoteRepository noteRepository, IPersonRepository personRepository)
        {
            _noteRepository = noteRepository;
            _personRepository = personRepository;
        }

        public async Task<ActionResult<IEnumerable<Note>>> GetNotesAsync()
        {
            using (MiniProfiler.Current.Step(nameof(GetNotesAsync)))
            {
                var notes = await _noteRepository.LoadNotesAsync();
                var scope = AuditScope.Create($"NoteService:{GetNotesAsync}", () => notes);

                return new OkObjectResult(notes);
            }
        }

        public async Task<ActionResult<Note>> GetNoteAsync(int id)
        {
            using (MiniProfiler.Current.Step(nameof(GetNoteAsync)))
            {
                var note = await _noteRepository.FindNoteAsync(id);
                var scope = AuditScope.Create($"Note:{GetNoteAsync}", () => note);

                if (note is null)
                {
                    return new NotFoundResult();
                }

                return note;
            }
        }

        public async Task<IActionResult> PutNoteAsync(int id, Note note)
        {
            using (MiniProfiler.Current.Step(nameof(PutNoteAsync)))
            {
                if (id != note.Id)
                {
                    return new BadRequestResult();
                }

                try
                {
                    await _noteRepository.UpdateNoteAsync(id, note);
                    var scope = AuditScope.Create($"Note:{PutNoteAsync}", () => note);
                }
                catch (Exception ex)
                {
                    if (!await _noteRepository.NoteExistsAsync(id))
                    {
                        return new NotFoundResult();
                    }
                    else
                    {
                        return new UnprocessableEntityObjectResult(new ProblemDetails
                        {
                            Title = "Note update could not be processed.",
                            Detail = $"{ex.Message}"
                        });
                    }
                }

                return new NoContentResult();
            }
        }

        public async Task<ActionResult<Note>> PostNoteAsync(Note note)
        {
            using (MiniProfiler.Current.Step(nameof(PostNoteAsync)))
            {
                await _noteRepository.AddNoteAsync(note);
                var scope = AuditScope.Create($"Note:{PostNoteAsync}", () => note);

                return new OkObjectResult(note);
            }
        }

        public async Task<IActionResult> DeleteNoteAsync(int personId, int id)
        {
            using (MiniProfiler.Current.Step(nameof(DeleteNoteAsync)))
            {
                var note = await _noteRepository.FindNoteAsync(id);
                if (note is null)
                {
                    return new NotFoundResult();
                }

                var person = await _personRepository.FindPersonAsync(personId);
                if (person is null)
                {
                    return new UnprocessableEntityObjectResult(new ProblemDetails
                    {
                        Title = "Note deletion could not be processed.",
                        Detail = $"The person with ID {personId} could not be found."
                    });
                }
                else if (!person.IsAdmin)
                {
                    return new UnprocessableEntityObjectResult(new ProblemDetails
                    {
                        Title = "Note deletion could not be processed.",
                        Detail = $"The person with ID {personId} is not an administrator."
                    });
                }

                await _noteRepository.RemoveNoteAsync(note);
                var scope = AuditScope.Create($"Note:{DeleteNoteAsync}", () => note);

                return new NoContentResult();
            }
        }
    }
}
