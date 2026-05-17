using System;
using System.Collections.Generic;

namespace WebBanSanPhamCongNghe.Areas.Admin.Data;

public partial class Review
{
    public int Id { get; set; }

    public int? Rate { get; set; }

    public string? Message { get; set; }

    public DateTime? CreateAt { get; set; }

    public int? ProductId { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
