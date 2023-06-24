using API.Utilities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace API.Model
{
    [Table("tb_m_reports")]
    public class Report : BaseEntity
    {
        [Column("subject", TypeName = "nvarchar(100)")]
        public string Subject { get; set; }

        [Column("description", TypeName ="text")]
        public string Description { get; set; }
        [Column("file_name", TypeName = "nvarchar(100)")]
        public string FileName { get; set; }

        [Column("file_data", TypeName = "varbinary(MAX)")]
        public byte[] FileData { get; set; }
        [Column("file_type")]
        public FileType FileType { get; set; }

        //Cardinalitas Below Here
        public Task? Task { get; set; }
/*        public File? File { get; set; }*/
    }

}
