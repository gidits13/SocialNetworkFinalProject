using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkFinalProject.ViewModels.Account
{
    public class UserEditViewModel
    {
        [Required]
        [Display(Name ="ID пользователя")]
        public string UserId { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Фамилия")]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Отчество", Prompt = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Email", Prompt = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения", Prompt = "дата Рождения")]
        public DateTime BirthDate { get; set; }
        
        [Display(Name = "Ссылка на фото", Prompt = "Ссылка на фото")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [Display(Name ="Статус", Prompt = "Статус")]
        [DataType(DataType.Text)]
        public string Status { get; set; }

        [Display(Name ="О себе", Prompt = "О себе")]
        [DataType(DataType.Text)]
        public string About { get; set; }
    }
}
