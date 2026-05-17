using WebBanSanPhamCongNghe.Areas.Admin.Data;
using WebBanSanPhamCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebBanSanPhamCongNghe.ViewComponents
{
    public class RelatedProductViewComponent : ViewComponent
    {
        private readonly MyShopContext _context;

        public RelatedProductViewComponent(MyShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var currentProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            var relatedProduct = await _context.Products
                .Where(p => p.Id != currentProduct.Id && p.Category.Id == currentProduct.Category.Id)
                .Select(p => new ProductViewModel 
                { 
                    Id = p.Id,
                    Img = p.Img,
                    Title = p.Title,
                    Content = p.Content,
                    Price = p.Price,
                    Rate = p.Rate,
                    CategoryTitle = p.Category.Title
                })
                .OrderByDescending(p => p.Rate)
                .ToListAsync();

            return View("Default", relatedProduct);
        }
    }
}
