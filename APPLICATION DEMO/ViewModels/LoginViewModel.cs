using System.ComponentModel.DataAnnotations;

namespace APPLICATION_DEMO.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User Name Is Required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
