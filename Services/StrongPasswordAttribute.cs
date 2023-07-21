using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class StrongPasswordAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string password = value as string;
        if (string.IsNullOrEmpty(password))
        {
            return new ValidationResult("Password is required.");
        }

        // Define your custom strong password rules here
        if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
        {
            return new ValidationResult("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.");
        }

        return ValidationResult.Success;
    }
}
