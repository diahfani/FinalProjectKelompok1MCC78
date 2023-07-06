namespace Client.Models
{
    public class TaskRatingViewModel
    {
        /*public Guid Guid { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public Guid EmployeeGuid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }*/
        public IEnumerable<Task> Tasks { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
    }
}
