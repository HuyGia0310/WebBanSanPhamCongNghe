using System;
using System.Collections.Generic;

namespace WebBanSanPhamCongNghe.Areas.Admin.Data;

public partial class Cart
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? CreateAt { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Product? Product { get; set; }
}
