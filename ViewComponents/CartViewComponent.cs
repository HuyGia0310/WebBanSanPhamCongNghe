using WebBanSanPhamCongNghe.Helper;
using WebBanSanPhamCongNghe.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebBanSanPhamCongNghe.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Lấy danh sách giỏ hàng
            var cart = HttpContext.Session.Get<List<CartItem>>(MyConst.CART_KEY) ?? new List<CartItem>();
            return View(new CartModel()
            {
                Quantity = cart.Sum(p => p.Quantity),
                Total = cart.Sum(p => p.Total)
            });
        }
    }
}
