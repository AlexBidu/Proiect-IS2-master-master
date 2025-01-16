using TicketingSystem.DTOs;

namespace TicketingSystem.Services
{
    public interface ITicketService
    {
        Task<TicketDto> CreateTicket(CreateTicketDto createTicketDto);
        Task<List<TicketDto>> GetTickets();
        Task<TicketDto> GetTicket(int id);
        Task<TicketDto> UpdateTicket(int id, UpdateTicketDto updateTicketDto);
        Task DeleteTicket(int id);
    }
}