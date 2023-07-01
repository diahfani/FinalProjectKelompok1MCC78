using Client.Utilities;

namespace Client.ViewModels;

public class ReportVM
{
    public Guid Guid { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public string FileName { get; set; }
    public byte[] FileData { get; set; }
    public FileType FileType { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
