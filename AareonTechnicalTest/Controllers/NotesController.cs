using AareonTechnicalTest.Models;
using AareonTechnicalTest.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            return await _noteService.GetNotesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            return await _noteService.GetNoteAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, Note note)
        {
            return await _noteService.PutNoteAsync(id, note);
        }

        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            return await _noteService.PostNoteAsync(note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int personId, int id)
        {
            return await _noteService.DeleteNoteAsync(personId, id);
        }
    }
}
