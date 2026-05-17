using WebBanSanPhamCongNghe.Areas.Admin.Data;
using WebBanSanPhamCongNghe.Helper;
using WebBanSanPhamCongNghe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebBanSanPhamCongNghe.Controllers
{
    public class ContactController : Controller
    {
        private readonly MyShopContext _context;

        public ContactController(MyShopContext context)
        {
            _context = context;
        }

        // GET: ContactController
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.CreateAt = DateTime.Now;
                _context.Add(contact);
                await _context.SaveChangesAsync();
                TempData["SubmitContactFormSuccess"] = "Your message has been sent successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["SubmitContactFormError"] = "There was an error sending your message. Please try again later.";
            return View("Index",contact);
        }
    }
}
