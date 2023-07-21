using System.ComponentModel.DataAnnotations;

public class AllowedRolesAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string role = value as string;
        if (string.IsNullOrEmpty(role))
        {
            return new ValidationResult("Role is required.");
        }

        // Define your allowed roles here
        string[] allowedRoles = new string[] { "Admin", "Client" };

        if (!allowedRoles.Contains(role.ToLower()))
        {
            return new ValidationResult($"Role '{role}' is not allowed. Allowed roles are: {string.Join(", ", allowedRoles)}.");
        }

        return ValidationResult.Success;
    }
}
