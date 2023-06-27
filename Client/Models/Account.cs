using MessagePack.Formatters;

namespace Client.Models
{
    public class Account
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
