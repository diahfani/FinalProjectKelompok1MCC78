using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    public class File
    {
        public Guid? Guid { get; set; }

        public string Name { get; set; }

        public byte[] Data { get; set; }
        public int Type { get; set; }

    }
}
