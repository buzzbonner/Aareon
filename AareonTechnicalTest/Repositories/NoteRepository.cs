using AareonTechnicalTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationContext _context;

        public NoteRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> LoadNotesAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Note> FindNoteAsync(int id)
        {
            return await _context.Notes.FindAsync(id);
        }

        public async Task UpdateNoteAsync(int id, Note note)
        {
            _context.Entry(note).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> NoteExistsAsync(int id)
        {
            return await _context.Notes.AnyAsync(e => e.Id == id);
        }

        public async Task AddNoteAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveNoteAsync(Note note)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }
    }
}
