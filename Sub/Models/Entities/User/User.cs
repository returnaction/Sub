using Microsoft.AspNetCore.Identity;

namespace Sub.Models.Entities.User
{
    public class User : IdentityUser
    {
        public bool IsActive { get; set; } = true;
        public bool IsSuperuser { get; set; } = false;
        public bool IsVerified { get; set; } = false;

        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? MiddleName { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; } = null!;

        // picture


    }
}
