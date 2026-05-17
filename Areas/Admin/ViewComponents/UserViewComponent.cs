using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;
using System.Security.Claims;
using WebBanSanPhamCongNghe.Areas.Admin.Data;

namespace WebBanSanPhamCongNghe.Areas.Admin.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        private readonly MyShopContext _context;

        public UserViewComponent(MyShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var id = HttpContext.User.FindFirstValue("CustomerId");
            var user = await _context.Customers.FindAsync(Convert.ToInt32(id));
            return View("Default", user);
        }
    }
}
