namespace Client.Models
{
    public class Feedback
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiededDate { get; set; }
        public int Status { get; set; }
    }
}
