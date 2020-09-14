using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Api.Data.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Поле електронна пошта є обов'язковим")]
        [EmailAddress(ErrorMessage = "Введене поле не є правильним для електронної пошти")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле пароль є обов'язковим")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }

    }
}
