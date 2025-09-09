namespace ConcertApi.Models
{
    public class Concert
    {
        public required string Id { get; set; }
        public string Name { get; set; } = "N/A";
        public string Venue { get; set; } = "N/A";
        public DateTime Date { get; set; }
        public string City { get; set; } = "N/A";
        public decimal TicketPrice { get; set; }
        public int Seats { get; set; }
    }
}