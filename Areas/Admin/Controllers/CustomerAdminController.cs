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
    public class CustomerAdminController : Controller
    {
        private readonly MyShopContext _context;

        public CustomerAdminController(MyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/CustomerAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Admin/CustomerAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Admin/CustomerAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Admin/CustomerAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile ImageUpload, [Bind("Id,FirstName,LastName,Address,Phone,Email,Img,RegisteredAt,UpdateAt,DateOfBirth,Password,RandomKey,IsActive,Role")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            var existingCustomer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            customer.RegisteredAt = existingCustomer.RegisteredAt;
            customer.Password = existingCustomer.Password;
            customer.RandomKey = existingCustomer.RandomKey;

            customer.UpdateAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageUpload != null)
                    {
                        customer.Img = ImageHelper.UpLoadImage(ImageUpload, "Customer");
                    }
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }


        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
