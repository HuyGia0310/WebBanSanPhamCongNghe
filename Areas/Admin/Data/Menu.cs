using System;
using System.Collections.Generic;

namespace WebBanSanPhamCongNghe.Areas.Admin.Data;

public partial class Menu
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string? Title { get; set; }

    public string? MenuUrl { get; set; }

    public int? MenuIndex { get; set; }

    public bool? IsVisible { get; set; }
}
