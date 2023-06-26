using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    public class Task
    {
        public Guid? Guid { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

    }
}
