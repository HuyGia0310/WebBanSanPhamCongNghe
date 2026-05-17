using System;
using System.Collections.Generic;

namespace WebBanSanPhamCongNghe.Areas.Admin.Data;

public partial class PaymentDetail
{
    public int ProductId { get; set; }

    public int PaymentId { get; set; }

    public int? Price { get; set; }

    public int? Quantity { get; set; }

    public double? Total { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual Payment Payment { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
