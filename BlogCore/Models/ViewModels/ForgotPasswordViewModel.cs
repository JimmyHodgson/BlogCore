using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
