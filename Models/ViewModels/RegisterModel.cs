using System.ComponentModel.DataAnnotations;

namespace nadis.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Не указан Имя пользователя")]
        public string username { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string userpassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("userpassword", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        public string KIDro { get; set; }
        public string Role { get; set; }
    }
}