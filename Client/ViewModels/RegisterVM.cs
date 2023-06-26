using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Full Name")]
        public string Fullname { get; set; }

        //public GenderLevel Gender { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string HiringDate { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
