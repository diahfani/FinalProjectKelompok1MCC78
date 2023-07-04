namespace Client.Models
{
    public class TaskRatingViewModel
    {
        public IEnumerable<Task> Tasks { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
    }
}
