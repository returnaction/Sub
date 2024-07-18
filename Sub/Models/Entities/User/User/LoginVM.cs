namespace Sub.Models.Entities.User.User
{
    public class LoginVM
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; } = true;

    }
}
