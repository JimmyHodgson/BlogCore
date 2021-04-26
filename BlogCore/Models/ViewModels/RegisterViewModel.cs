using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models.ViewModels
{
    public class RegisterViewModel
    {
        public bool RegisterEnabled { get; set; } = false;
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
