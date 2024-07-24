using System.ComponentModel.DataAnnotations;

namespace Sub.Models.Entities.User.User
{
    public class ForgotPasswrodVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

    }
}
