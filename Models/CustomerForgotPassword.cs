using System.ComponentModel.DataAnnotations;

namespace WebBanSanPhamCongNghe.Models
{
    public class CustomerForgotPassword
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string Email { get; set; }

        public string RandomCode { get; set; }

        [Display(Name = "OTP")]
        [Required(ErrorMessage = "*")]
        [MaxLength(10, ErrorMessage = "Tối đa 10 kí tự")]
        public string OTP { get; set; }

    }
}
