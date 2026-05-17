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
    public class CategoryAdminController : Controller
    {
        private readonly MyShopContext _context;

        public CategoryAdminController(MyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/CategoryAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Admin/CategoryAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/CategoryAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoryAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile Img,[Bind("Id,Title,Content,CreateAt,UpdateAt,Img")] Category category)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.Now;
                category.CreateAt = now;
                category.UpdateAt = now;
                if (Img != null)
                {
                    category.Img = ImageHelper.UpLoadImage(Img, "Category");
                }         
                else
                {
                    category.Img = "no-image.png";
                }    
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/CategoryAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/CategoryAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile ImageUpload, [Bind("Id,Title,Content,CreateAt,UpdateAt,Img")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            var existingCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            category.CreateAt = existingCategory.CreateAt;

            category.UpdateAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageUpload != null)
                    {
                        category.Img = ImageHelper.UpLoadImage(ImageUpload, "Category");
                    }
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Admin/CategoryAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/CategoryAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            try
            {
                if (category != null)
                {
                    _context.Categories.Remove(category);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                TempData["Error"] = "Error!";
                return View("Delete", category);
            }
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
