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
    public class ContactAdminController : Controller
    {
        private readonly MyShopContext _context;

        public ContactAdminController(MyShopContext context)
        {
            _context = context;
        }

        // GET: Admin/ContactAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contacts.ToListAsync());
        }
    }
}
