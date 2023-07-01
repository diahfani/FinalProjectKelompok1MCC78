using Client.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.ViewModels
{
    public class FileUploadAndDownlodVM
    {
        public Guid Guid { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public IFormFile FileName { get; set; }
        public FileType FileType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
