using API.Contracts;
using API.Model;
using API.ViewModel.Report;
using API.ViewModel.Task;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static NuGet.Client.ManagedCodeConventions;

namespace API.Utilities
{
    public class ReportValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var report = value as ReportVM;

            if (report == null)
            {
                return ValidationResult.Success; // If the value is not a Report object, consider it as valid
            }

            var task = value as TaskVM;
            var employeeId = task.EmployeeGuid;
            var taskId = task.Guid;
            var reports = taskId;


            if (task == null)
            {
                return new ValidationResult("Invalid task."); // Return a validation error if the task is not found
            }

            if (task.Deadline <= DateTime.Now)
            {
                return new ValidationResult("Cannot upload report for a task that has passed the deadline."); // Return a validation error if the task's deadline has passed
            }

            return ValidationResult.Success; // Validation succeeded
        }
    }
}
