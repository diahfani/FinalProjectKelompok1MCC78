using Client.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class RegisterVM
    {

        public string Fullname { get; set; }
        public string NIK { get; set; }
        public string Email { get; set; }
        public GenderLevel Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public Guid? ManagerID { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

}
