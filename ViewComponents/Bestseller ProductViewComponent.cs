using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanSanPhamCongNghe.Areas.Admin.Data;
using WebBanSanPhamCongNghe.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebBanSanPhamCongNghe.ViewComponents
{
    public class BestsellerProductViewComponent : ViewComponent
    {
        private readonly MyShopContext _context;

        public BestsellerProductViewComponent(MyShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Lấy danh sách sản phẩm bán chạy
            var bestSellerProducts = await _context.PaymentDetails
                .GroupBy(pd => pd.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    TotalQuantity = group.Sum(pd => pd.Quantity ?? 0)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Join(_context.Products,
                      pd => pd.ProductId,
                      product => product.Id,
                      (pd, product) => new ProductViewModel
                      {
                          Id = product.Id,
                          Title = product.Title,
                          Content = product.Content,
                          Img = product.Img,
                          Price = product.Price,
                          Rate = product.Rate ?? 0,
                          CategoryId = product.CategoryId ?? 0,
                          CategoryTitle = product.Category.Title
                      })
                .ToListAsync();

            return View("Default", bestSellerProducts);
        }
    }
}
