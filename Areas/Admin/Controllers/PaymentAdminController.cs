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
    public class PaymentAdminController : Controller
    {
        private readonly MyShopContext _context;

        public PaymentAdminController(MyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/PaymentAdmin
        public async Task<IActionResult> Index()
        {
            var payment = _context.Payments.Include(p => p.Cart).ThenInclude(c => c.Customer); 
            return View(await payment.ToListAsync());
        }

        // GET: Admin/PaymentAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Cart)
                .ThenInclude(c => c.Customer)
                .Include(p => p.PaymentDetails)
                .ThenInclude(pd => pd.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

    }
}
