using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebBanSanPhamCongNghe.Areas.Admin.Data;

namespace WebBanSanPhamCongNghe.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Img { get; set; }
        public int? Price { get; set; }
        public double? Rate { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryTitle { get; set; }
        public string? SelectedSort { get; set; }
        public SelectList SortOptions { get; set; } 
        public List<Review>? ReviewList { get; set; }
    }
}
