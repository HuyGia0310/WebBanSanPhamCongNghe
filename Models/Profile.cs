using System.ComponentModel.DataAnnotations;

namespace WebBanSanPhamCongNghe.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Họ")]
        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự")]
        public string LastName { get; set; } = null!;
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "*")]
        [MaxLength(500, ErrorMessage = "Tối đa 500 kí tự")]
        public string Address { get; set; } = null!;
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "*")]
        [MaxLength(15, ErrorMessage = "Tối đa 15 kí tự")]
        public string Phone { get; set; } = null!;
        [Display(Name = "Email")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string Email { get; set; } = null!;
        public string? Img { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateOnly? DateOfBirth { get; set; }
    }
}
