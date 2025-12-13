namespace TravelTourConsole.Models
{
    public class Tour
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public string DestinationCity { get; set; }
    }
}