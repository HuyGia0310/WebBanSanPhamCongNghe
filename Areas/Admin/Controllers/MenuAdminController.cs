using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanSanPhamCongNghe.Areas.Admin.Data;

namespace WebBanSanPhamCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class MenuAdminController : Controller
    {
        private readonly MyShopContext _context;

        public MenuAdminController(MyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/MenuAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Menus.ToListAsync());
        }

        // GET: Admin/MenuAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Admin/MenuAdmin/Create
        public IActionResult Create(int? parentId)
        {
            ViewData["parentId"] = parentId;
            return View();
        }

        // POST: Admin/MenuAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? parentId, [Bind("Id,ParentId,Title,MenuUrl,MenuIndex,IsVisible")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(menu.MenuUrl) && !menu.MenuUrl.StartsWith("/"))
                {
                    menu.MenuUrl = "/" + menu.MenuUrl;
                }

                // Xử lý ParentId và MenuIndex
                if (parentId != null)
                {
                    menu.ParentId = parentId;
                    menu.MenuIndex = await _context.Menus
                        .Where(m => m.ParentId == menu.ParentId)
                        .MaxAsync(m => (int?)m.MenuIndex) ?? 0; // Nếu không có giá trị thì bắt đầu từ 0
                    menu.MenuIndex += 1; // Tăng lên 1
                }
                else
                {
                    menu.ParentId = null;
                    menu.MenuIndex = await _context.Menus
                        .Where(m => m.ParentId == null)
                        .MaxAsync(m => (int?)m.MenuIndex) ?? 0; // Nếu không có giá trị thì bắt đầu từ 0
                    menu.MenuIndex += 1; // Tăng lên 1
                }

                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Admin/MenuAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            var menuIndexes = await _context.Menus
                                            .Where(m => m.ParentId == menu.ParentId)
                                            .Select(m => m.MenuIndex)
                                            .OrderBy(m => m)
                                            .ToListAsync();

            // Tạo SelectList cho MenuIndex
            ViewData["MenuIndexList"] = new SelectList(menuIndexes, menu.MenuIndex);

            return View(menu);
        }

        // POST: Admin/MenuAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentId,Title,MenuUrl,MenuIndex,IsVisible")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            var originalMenu = await _context.Menus.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (originalMenu == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    if (menu.MenuIndex != originalMenu.MenuIndex)
                    {
                        var swapMenuIndex = await _context.Menus
                            .FirstOrDefaultAsync(m => m.ParentId == menu.ParentId && m.MenuIndex == menu.MenuIndex);

                        if (swapMenuIndex != null)
                        {
                            swapMenuIndex.MenuIndex = originalMenu.MenuIndex;
                            _context.Update(swapMenuIndex);
                        }    
                    }    
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            var menuIndexes = await _context.Menus
                                            .Where(m => m.ParentId == menu.ParentId)
                                            .Select(m => m.MenuIndex)
                                            .OrderBy(m => m)
                                            .ToListAsync();

            ViewData["MenuIndexList"] = new SelectList(menuIndexes, menu.MenuIndex);

            return View(menu);
        }


        // GET: Admin/MenuAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Admin/MenuAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}
