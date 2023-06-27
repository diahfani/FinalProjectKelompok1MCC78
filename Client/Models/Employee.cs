using Client.Utilities;
using System.ComponentModel.DataAnnotations.Schema;
namespace Client.Models
{
    public class Employee
    {
        public Guid Guid { get; set; }
        public string NIK { get; set; }
        public string Fullname { get; set; }
        public GenderLevel Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid? ManagerID { get; set; }


    }
}
