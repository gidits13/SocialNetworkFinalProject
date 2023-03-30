using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkFinalProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [Display(Name ="Имя")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailReg { get; set; }
        
        [Required]
        [Display(Name = "Год")]
        public int Year { get; set; }
        
        [Required]
        [Display(Name = "Число")]
        public int Date { get; set; }
        
        [Required]
        [Display(Name = "Месяц")]
        public int Month { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100,ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.",MinimumLength =8)]
        public string PasswordReg { get; set; }
        
        [Required]
        [Compare("PasswordReg", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name ="Подтвердите пароль")]
        public string PasswordConfirm { get; set; }

        public string Login => EmailReg;
        
    }
}
