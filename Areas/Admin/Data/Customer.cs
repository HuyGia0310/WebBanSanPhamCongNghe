using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebBanSanPhamCongNghe.Areas.Admin.Data;

public partial class Customer
{
    public int Id { get; set; }
    [DisplayName("First Name")]
    public string? FirstName { get; set; }
    [DisplayName("Last Name")]
    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }
    [DisplayName("Image")]
    public string? Img { get; set; }

    public DateTime? RegisteredAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Password { get; set; }

    public string? RandomKey { get; set; }

    public bool? IsActive { get; set; }

    public int? Role { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
