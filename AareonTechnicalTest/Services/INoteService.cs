using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Services
{
    public interface INoteService
    {
        Task<ActionResult<IEnumerable<Note>>> GetNotesAsync();

        Task<ActionResult<Note>> GetNoteAsync(int id);

        Task<IActionResult> PutNoteAsync(int id, Note note);

        Task<ActionResult<Note>> PostNoteAsync(Note note);

        Task<IActionResult> DeleteNoteAsync(int personId, int id);
    }
}