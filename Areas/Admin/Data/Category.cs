using System;
using System.Collections.Generic;

namespace WebBanSanPhamCongNghe.Areas.Admin.Data;

public partial class Category
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? Img { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
