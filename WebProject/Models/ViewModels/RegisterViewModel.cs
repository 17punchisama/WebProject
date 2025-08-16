using System.ComponentModel.DataAnnotations;
using WebProject.Models.Helper;

namespace WebProject.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string ImageURL { get; set; } = "https://static.vecteezy.com/system/resources/previews/009/292/244/non_2x/default-avatar-icon-of-social-media-user-vector.jpg";
        [Required]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string TelNumber { get; set; }

        [Required(ErrorMessage = "กรุณาเลือกชั้นปี")]
        public int Year { get; set; }

        [Required(ErrorMessage = "กรุณาเลือกเพศ")]
        public Gender Gender { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "รหัสผ่านและยืนยันรหัสผ่านไม่ตรงกัน")]
        public string ConfirmPassword { get; set; }
    }
}
