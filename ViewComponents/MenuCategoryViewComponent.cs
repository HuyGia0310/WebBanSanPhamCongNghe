using WebBanSanPhamCongNghe.Areas.Admin.Data;
using WebBanSanPhamCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebBanSanPhamCongNghe.ViewComponents
{
    public class MenuCategoryViewComponent : ViewComponent
    {
        private readonly MyShopContext _context;

        public MenuCategoryViewComponent(MyShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryMenu = await _context.Categories
                .Select(c => new CategoryMenu
                {
                    Id = c.Id,
                    Title = c.Title,
                    Content = c.Content,
                    Count = _context.Products.Count(p => p.CategoryId == c.Id),
                })
                .OrderByDescending(c => c.Count)
                .ToListAsync();

            return View("Default", categoryMenu);
        }
    }
}
