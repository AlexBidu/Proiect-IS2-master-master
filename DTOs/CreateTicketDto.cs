namespace TicketingSystem.DTOs
{
    public class CreateTicketDto
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}