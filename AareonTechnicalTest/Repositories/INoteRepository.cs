using AareonTechnicalTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Repositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> LoadNotesAsync();

        Task<Note> FindNoteAsync(int id);

        Task UpdateNoteAsync(int id, Note note);

        Task<bool> NoteExistsAsync(int id);

        Task AddNoteAsync(Note note);

        Task RemoveNoteAsync(Note note);
    }
}