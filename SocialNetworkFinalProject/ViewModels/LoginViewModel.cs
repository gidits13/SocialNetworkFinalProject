using System.ComponentModel.DataAnnotations;

namespace SocialNetworkFinalProject.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Login")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }
        [Display(Name = "IsPersistant")]
        public bool IsPersistant { get; set; }

        public string ReturnURL { get; set; }
    }
}
