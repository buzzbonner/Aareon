using AareonTechnicalTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> LoadTicketsAsync();

        Task<Ticket> FindTicketAsync(int id);

        Task UpdateTicketAsync(int id, Ticket ticket);

        Task<bool> TicketExistsAsync(int id);

        Task AddTicketAsync(Ticket ticket);

        Task RemoveTicketAsync(Ticket ticket);
    }
}