using System;
using System.Collections.Generic;

namespace WebBanSanPhamCongNghe.Areas.Admin.Data;

public partial class Payment
{
    public int Id { get; set; }

    public DateTime? CreateAt { get; set; }

    public double? Total { get; set; }

    public int? CartId { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual ICollection<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();
}
