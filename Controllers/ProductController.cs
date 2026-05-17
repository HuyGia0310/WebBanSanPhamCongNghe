using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanSanPhamCongNghe.Areas.Admin.Data;
using WebBanSanPhamCongNghe.Models;
using X.PagedList.Extensions;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Policy;

namespace WebBanSanPhamCongNghe.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyShopContext _context;

        public ProductController(MyShopContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index(int? idCategory, string? search, string? sort = "", int page = 1, int pageSize = 9)
        {
            ViewData["Search"] = search;
            ViewData["Sort"] = sort;
            ViewData["IdCategory"] = idCategory;

            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (idCategory.HasValue)
            {
                query = query.Where(p => p.CategoryId == idCategory.Value);
            }

            // Áp dụng tìm kiếm theo từ khóa
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p =>
                    p.Title.Contains(search) ||
                    p.Content.Contains(search) ||
                    p.Rate.Value.ToString().Contains(search) ||
                    p.Price.Value.ToString().Contains(search) ||
                    p.Category.Title.Contains(search)
                );
            }

            // Tạo danh sách tùy chọn sắp xếp
            var sortOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Mặc Định" },
                new SelectListItem { Value = "price_asc", Text = "Giá Thấp Nhất" },
                new SelectListItem { Value = "price_desc", Text = "Giá Cao Nhất" },
                new SelectListItem { Value = "rate_desc", Text = "Đánh Giá Cao" }
            };

            // Xử lý sắp xếp theo giá hoặc đánh giá
            switch (sort)
            {
                case "price_asc":
                    query = query.OrderBy(p => p.Price); // Sắp xếp giá từ thấp đến cao
                    break;
                case "price_desc":
                    query = query.OrderByDescending(p => p.Price); // Sắp xếp giá từ cao đến thấp
                    break;
                case "rate_desc":
                    query = query.OrderByDescending(p => p.Rate); // Sắp xếp đánh giá từ cao đến thấp
                    break;
                default:
                    query = query.OrderByDescending(p => p.CreateAt); // Mặc định: sắp xếp theo ngày nhập 
                    break;
            }

            var products = await query
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    Img = p.Img,
                    Price = p.Price,
                    Rate = p.Rate,
                    CategoryTitle = p.Category.Title
                }).ToListAsync();

            ViewData["SortOptions"] = new SelectList(sortOptions, "Value", "Text", sort);

            var pagedProducts = products.ToPagedList(page, pageSize);
            return View(pagedProducts);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return Redirect("/404");
            }

            // Lấy thông tin sản phẩm
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return Redirect("/404");
            }

            // Lấy danh sách review
            var review = await _context.Reviews.Where(r => r.ProductId == id).OrderBy(r => r.CreateAt).Include(c => c.Customer).ToListAsync();

            var productViewModel = new ProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Content = product.Content,
                Img = product.Img,
                Price = product.Price,
                Rate = product.Rate,
                CategoryTitle = product.Category.Title,
                ReviewList = review ?? null
            };

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitReviewForm(ReviewViewModel review)
        {

            // Lấy CustomerId từ Claims
            string? idTam = HttpContext.User.FindFirstValue("CustomerId");

            if (string.IsNullOrEmpty(idTam))
            {
                return RedirectToAction("SignIn", "Customer");
            }

            int idCustomer = Convert.ToInt32(idTam);

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == idCustomer);
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == review.ProductId);

            if (customer == null || product == null)
            {
                return Redirect("/404");
            }

            if (ModelState.IsValid)
            {
                var newReview = new Review
                {
                    CreateAt = DateTime.Now,
                    Rate = review.Rate,
                    Message = review.Message,
                    ProductId = review.ProductId,
                    CustomerId = customer.Id,
                };
                _context.Add(newReview);
                await _context.SaveChangesAsync();

                product.Rate = _context.Reviews.Where(r => r.ProductId == product.Id).Average(r => r.Rate);
                _context.Update(product);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Cảm Ơn Bạn Đã Đánh Giá Sản Phẩm.";
                return RedirectToAction("Details", new { id = review.ProductId });
            }

            TempData["Error"] = "Lỗi";
            return RedirectToAction("Details", new { id = review.ProductId });
        }
    }
}
