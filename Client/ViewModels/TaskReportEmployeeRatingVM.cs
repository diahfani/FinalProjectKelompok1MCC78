using Client.Models;

namespace Client.ViewModels
{
    public class TaskReportEmployeeRatingVM
    {
        public Guid Guid { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public Guid EmployeeGuid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public EmployeeVM Employee { get; set; }
        public ReportVM? Report { get; set; }
        public Rating? Rating { get; set; }
    }
}
