using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    public class Rating
    {
        public Guid Guid { get; set; }
        public float RatingValue { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
