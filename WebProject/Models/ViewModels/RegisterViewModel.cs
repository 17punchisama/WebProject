using System.ComponentModel.DataAnnotations;
using WebProject.Models.Helper;

namespace WebProject.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string TelNumber { get; set; }

        [Required(ErrorMessage = "กรุณาเลือกชั้นปี")]
        public string Year { get; set; }

        [Required(ErrorMessage = "กรุณาเลือกเพศ")]
        public Gender Gender { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
