using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanSanPhamCongNghe.Areas.Admin.Data;
using WebBanSanPhamCongNghe.Models;

namespace WebBanSanPhamCongNghe.ViewComponents
{
    public class CarouselCategory : ViewComponent
    {
        private readonly MyShopContext _context;

        public CarouselCategory(MyShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Default", await _context.Categories.ToListAsync());
        }
    }
}

