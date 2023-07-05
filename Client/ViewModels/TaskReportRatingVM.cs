using Client.Models;

namespace Client.ViewModels;

public class TaskReportRatingVM
{
    public Models.Task Task { get; set; }
    public Report Report
    {
        get; set;
    }
    public Rating? Rating { get; set; }
}
