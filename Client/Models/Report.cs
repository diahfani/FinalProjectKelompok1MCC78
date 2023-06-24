using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    public class Report
    {
        public Guid? Guid { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public byte[] File { get; set; }
    }
}
