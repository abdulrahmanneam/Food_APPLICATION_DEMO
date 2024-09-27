using System.ComponentModel.DataAnnotations;

namespace APPLICATION_DEMO.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "User Name Is Required")]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        //[DataType(DataType.MultilineText)]
        //public string? Address { get; set; }
    }
}
