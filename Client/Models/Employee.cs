using System.ComponentModel.DataAnnotations.Schema;
namespace Client.Models
{
    public class Employee
    {
        public Guid? Guid { get; set; }
        public string NIK { get; set; }

        public string FullName { get; set; }

        //public GenderLevel Gender { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string HiringDate { get; set; }



    }
}
