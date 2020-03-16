using System.ComponentModel.DataAnnotations;

namespace AuthSample.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string username { get; set; }
         
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string userpassword { get; set; }
    }
}