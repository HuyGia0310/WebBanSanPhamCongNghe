using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanSanPhamCongNghe.Areas.Admin.Data;
using WebBanSanPhamCongNghe.Helper;

namespace WebBanSanPhamCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class ProductAdminController : Controller
    {
        private readonly MyShopContext _context;

        public ProductAdminController(MyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductAdmin
        public async Task<IActionResult> Index()
        {
            var myShopContext = _context.Products.Include(p => p.Category);
            return View(await myShopContext.ToListAsync());
        }

        // GET: Admin/ProductAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/ProductAdmin/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title");
            return View();
        }

        // POST: Admin/ProductAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile Img ,[Bind("Id,Title,Content,Img,Price,Rate,CreateAt,UpdateAt,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.Now;
                product.CreateAt = now;
                product.UpdateAt = now;
                product.Rate = 0;
                if (Img != null)
                {
                    product.Img = ImageHelper.UpLoadImage(Img, "Product");
                }    
                else
                {
                    product.Img = "no-image.png";
                }    
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", product.CategoryId);
            return View(product);
        }

        // GET: Admin/ProductAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", product.CategoryId);
            return View(product);
        }

        // POST: Admin/ProductAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile ImageUpload, [Bind("Id,Title,Content,Img,Price,Rate,CreateAt,UpdateAt,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            var existingProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            product.CreateAt = existingProduct.CreateAt;
            product.Rate = existingProduct.Rate;

            product.UpdateAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageUpload != null)
                    {
                        product.Img = ImageHelper.UpLoadImage(ImageUpload, "Product");
                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", product.CategoryId);
            return View(product);
        }

        // GET: Admin/ProductAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/ProductAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            try
            {
                if (product != null)
                {
                    _context.Products.Remove(product);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                TempData["Error"] = "Error!";
                return View("Delete", product);
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
