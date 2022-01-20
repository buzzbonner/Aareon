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
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsAsync()
        {
            using (MiniProfiler.Current.Step(nameof(GetTicketsAsync)))
            {
                var tickets = await _ticketRepository.LoadTicketsAsync();
                var scope = AuditScope.Create($"Ticket:{GetTicketsAsync}", () => tickets);

                return new OkObjectResult(tickets);
            }
        }

        public async Task<ActionResult<Ticket>> GetTicketAsync(int id)
        {
            using (MiniProfiler.Current.Step(nameof(GetTicketAsync)))
            {
                var note = await _ticketRepository.FindTicketAsync(id);
                var scope = AuditScope.Create($"Ticket:{GetTicketAsync}", () => note);

                if (note is null)
                {
                    return new NotFoundResult();
                }

                return note;
            }
        }

        public async Task<IActionResult> PutTicketAsync(int id, Ticket ticket)
        {
            using (MiniProfiler.Current.Step(nameof(PutTicketAsync)))
            {
                if (id != ticket.Id)
                {
                    return new BadRequestResult();
                }

                try
                {
                    await _ticketRepository.UpdateTicketAsync(id, ticket);
                    var scope = AuditScope.Create($"Ticket:{PutTicketAsync}", () => ticket);
                }
                catch (Exception ex)
                {
                    if (!await _ticketRepository.TicketExistsAsync(id))
                    {
                        return new NotFoundResult();
                    }
                    else
                    {
                        return new UnprocessableEntityObjectResult(new ProblemDetails
                        {
                            Title = "Ticket update could not be processed.",
                            Detail = $"{ex.Message}"
                        });
                    }
                }

                return new NoContentResult();
            }
        }

        public async Task<ActionResult<Ticket>> PostTicketAsync(Ticket ticket)
        {
            using (MiniProfiler.Current.Step(nameof(PostTicketAsync)))
            {
                await _ticketRepository.AddTicketAsync(ticket);
                var scope = AuditScope.Create($"Ticket:{PostTicketAsync}", () => ticket);

                return new OkObjectResult(ticket);
            }
        }

        public async Task<IActionResult> DeleteTicketAsync(int id)
        {
            using (MiniProfiler.Current.Step(nameof(DeleteTicketAsync)))
            {
                var ticket = await _ticketRepository.FindTicketAsync(id);
                if (ticket is null)
                {
                    return new NotFoundResult();
                }

                await _ticketRepository.RemoveTicketAsync(ticket);
                var scope = AuditScope.Create($"Ticket:{DeleteTicketAsync}", () => ticket);

                return new NoContentResult();
            }
        }
    }
}
