using Microsoft.EntityFrameworkCore;
using TicketingSystem.Data;
using TicketingSystem.DTOs;
using TicketingSystem.Entities;

namespace TicketingSystem.Services
{
    public class TicketService : ITicketService
    {
        private readonly TicketingDbContext _context;

        public TicketService(TicketingDbContext context)
        {
            _context = context;
        }

        public async Task<TicketDto> CreateTicket(CreateTicketDto createTicketDto)
        {
            var ticket = new Ticket
            {
                UserId = createTicketDto.UserId,
                CategoryId = createTicketDto.CategoryId,
                Title = createTicketDto.Title,
                Description = createTicketDto.Description,
                Status = "Open", // Statusul implicit este "Open"
                CreatedAt = DateTime.UtcNow
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return new TicketDto(ticket);
        }

        public async Task<List<TicketDto>> GetTickets()
        {
            return await _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Category)
                .Include(t => t.Comments)
                .ThenInclude(c => c.User)
                .Select(t => new TicketDto(t))
                .ToListAsync();
        }

        public async Task<TicketDto> GetTicket(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Category)
                .Include(t => t.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket with ID {id} not found.");
            }

            return new TicketDto(ticket);
        }

        public async Task<TicketDto> UpdateTicket(int id, UpdateTicketDto updateTicketDto)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket with ID {id} not found.");
            }

            ticket.CategoryId = updateTicketDto.CategoryId;
            ticket.Title = updateTicketDto.Title;
            ticket.Description = updateTicketDto.Description;
            ticket.Status = updateTicketDto.Status;

            await _context.SaveChangesAsync();

            return new TicketDto(ticket);
        }

        public async Task DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket with ID {id} not found.");
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }
    }
}