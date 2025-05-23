namespace EventService.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public DateTime Date { get; set; }
        public string Image { get; set; } = "";
        public int TicketsLeft { get; set; }
        public int TicketsSold { get; set; }
        public decimal Price { get; set; }


        public int TicketsSoldPercent => TicketsSold + TicketsLeft > 0
            ? (int)Math.Round(100.0m * TicketsSold / (TicketsSold + TicketsLeft))
            : 0;
    }
}
