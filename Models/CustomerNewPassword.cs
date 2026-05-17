using System.ComponentModel.DataAnnotations;

namespace WebBanSanPhamCongNghe.Models
{
    public class CustomerNewPassword
    {
        public string Email { get; set; }
        public string RandomKey { set; get; }

        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "*")]
        [MaxLength(200, ErrorMessage = "Tối đa 200 kí tự")]
        public string NewPassWord { set; get; }

        [Display(Name = "Nhập lại mật khẩu mới")]
        [Required(ErrorMessage = "*")]
        [MaxLength(200, ErrorMessage = "Tối đa 200 kí tự")]
        public string Confirm_NewPassWord { set; get; }

    }
}
