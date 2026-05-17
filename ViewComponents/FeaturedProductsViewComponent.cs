using WebBanSanPhamCongNghe.Areas.Admin.Data;
using WebBanSanPhamCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebBanSanPhamCongNghe.ViewComponents
{
    public class FeaturedProductsViewComponent : ViewComponent
    {
        private readonly MyShopContext _context;

        public FeaturedProductsViewComponent(MyShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? limit)
        {
            var product = await _context.Products
                .OrderByDescending(p => p.Rate)
                .Take(limit ?? 4)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Img = p.Img,
                    Title = p.Title,
                    Rate = p.Rate,
                    Price = p.Price,
                })
                .ToListAsync();
                
                

            return View("Default", product);
        }
    }
}
