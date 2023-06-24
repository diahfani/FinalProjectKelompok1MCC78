using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.ViewModel.Task
{
    public class TaskVM
    {
        public Guid? Guid { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public Guid EmployeeGuid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
