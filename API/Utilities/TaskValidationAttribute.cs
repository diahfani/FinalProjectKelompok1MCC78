using System.ComponentModel.DataAnnotations;

namespace API.Utilities
{
    public class TaskValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var deadline = (DateTime)value;
            var currentDate = DateTime.Now;

            return deadline > currentDate;
        }
    }
}
