﻿using Client.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    public class Report
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
}
