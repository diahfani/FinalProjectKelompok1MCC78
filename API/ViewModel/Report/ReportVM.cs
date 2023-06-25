using API.Utilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.ViewModel.Report;

public class ReportVM
{
    public Guid Guid { get; set; }
    public string SubjectReport { get; set; }
    public string DescriptionReport { get; set; }
    public string FileName { get; set; }
    public byte[] FileData { get; set; }
    public FileType FileType { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

}
