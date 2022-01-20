using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Services
{
    public interface ITicketService
    {
        Task<ActionResult<IEnumerable<Ticket>>> GetTicketsAsync();

        Task<ActionResult<Ticket>> GetTicketAsync(int id);

        Task<IActionResult> PutTicketAsync(int id, Ticket ticket);

        Task<ActionResult<Ticket>> PostTicketAsync(Ticket ticket);

        Task<IActionResult> DeleteTicketAsync(int id);
    }
}