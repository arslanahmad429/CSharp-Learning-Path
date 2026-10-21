namespace CampusEventManagement.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
    }
}
