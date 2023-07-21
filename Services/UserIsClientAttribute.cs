using System.ComponentModel.DataAnnotations;
using WebAPI.Data;

namespace WebAPI.Services
{
    internal class UserIsClientAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (SqlServerDataContext)validationContext.GetService(typeof(SqlServerDataContext));
            var userId = (int)value;

            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId && u.Role == "Client");

            if (user == null)
            {
                return new ValidationResult("The user must be a client.");
            }

            return ValidationResult.Success;
        }
    }
}