using API.Utilities;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModel.Account;

public class RegisterVM
{
    [Required(ErrorMessage = "Fullname is required")]
    [Display(Name ="Full Name")]
    public string Fullname { get; set; }
    [Required(ErrorMessage = "NIK is required")]
    [Display(Name = "NIK")]
    [NIKEmailPhoneValidation(nameof(NIK))]
    public string NIK { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    [NIKEmailPhoneValidation(nameof(Email))]
    public string Email { get; set; }
    [Required(ErrorMessage = "Gender is required")]

    public GenderLevel Gender { get; set; }
    [Phone]
    [Required(ErrorMessage ="Phone number is required")]
    [NIKEmailPhoneValidation(nameof(PhoneNumber))]
    public string PhoneNumber { get; set; }
    [Required]
    public DateTime HiringDate { get; set; }
    public Guid? ManagerID { get; set; }
    [Required]
    public string Password { get; set; }
    [Compare("Password")]
    [Required]
    public string ConfirmPassword { get; set; }
}
