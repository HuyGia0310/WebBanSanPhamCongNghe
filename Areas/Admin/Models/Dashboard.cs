using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebBanSanPhamCongNghe.Areas.Admin.Models
{
    public class Dashboard
    {
        public int? SelectedYear { get; set; }
        public int TotalPaymentsThisMonth { get; set; }
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int TotalUsers { get; set; }
        public List<int> PaymentsPerMonth { get; set; }
        public List<int> RevenuePerMonth { get; set; }
        public SelectList AvailableYears { get; set; }
    }

}


