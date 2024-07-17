using System.ComponentModel.DataAnnotations;

namespace Sub.Models.Entities.User.User
{
    public class SignUpVM
    {
        [Required(ErrorMessage = "Имя пользователя обязательно!")]
        [StringLength(25, ErrorMessage = "Имя пользователя 25 символов максимум!")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Имейл обязательно!")]
        [EmailAddress(ErrorMessage ="Неверный имейл!")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Пароль обязательно!")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль не меньше 6 и не больше 50 символов!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Подтверждение пароля обязательно!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; } = null!;

        // later I will make those not nullable for now lets have it as it is
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? MiddleName { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; } = null!;

    }
}
