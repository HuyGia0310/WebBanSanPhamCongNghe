using WebBanSanPhamCongNghe.Areas.Admin.Data;
using WebBanSanPhamCongNghe.Helper;
using WebBanSanPhamCongNghe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebBanSanPhamCongNghe.Controllers
{
    public class CartController : Controller
    {
        private readonly MyShopContext _context;

        public CartController(MyShopContext context)
        {
            _context = context;
        }

        private List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MyConst.CART_KEY) ?? new List<CartItem>();

        public IActionResult Index()
        {
            return View(Cart);
        }

        public async Task<IActionResult> AddToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.IdProduct == id);

            if (item == null)
            {
                var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    TempData["Message"] = "Không tìm thấy sản phẩm";
                    return Redirect("/404");
                }

                item = new CartItem
                {
                    IdProduct = product.Id,
                    Img = product.Img,
                    Name = product.Title,
                    Price = (int)product.Price,
                    Rate = (double)product.Rate,
                    Quantity = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }

            HttpContext.Session.Set(MyConst.CART_KEY, gioHang);

            return RedirectToAction("Index");
        }

        public IActionResult ChangeQuantityCart(int id, bool isIncrement = true, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.IdProduct == id);

            if (item == null)
            {
                TempData["Message"] = "Không tìm thấy sản phẩm";
                return Redirect("/404");
            }

            if (isIncrement)
            {
                item.Quantity += quantity;
            }
            else
            {
                item.Quantity -= quantity;
                if (item.Quantity <= 0)
                {
                    gioHang.Remove(item);  // Xóa sản phẩm khỏi giỏ hàng nếu số lượng <= 0
                }
            }

            HttpContext.Session.Set(MyConst.CART_KEY, gioHang);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.IdProduct == id);

            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MyConst.CART_KEY, gioHang);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> CheckOut()
        {
            try
            {
                if (!Cart.Any())
                {
                    TempData["CheckOutErrorMessage"] = "Giỏ hàng không có sản phẩm để thanh toán";
                    return RedirectToAction("Index");
                }

                string? customerIdStr = HttpContext.User.FindFirstValue("CustomerId");
                if (customerIdStr == null)
                {
                    return Redirect("/Customer/SignIn");
                }

                int customerId = Convert.ToInt32(customerIdStr);
                var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == customerId);

                if (customer == null)
                {
                    return Redirect("/404");
                }

                // Insert Payment vào cơ sở dữ liệu
                int cartId = await InsertCart(customerId);

                var paymentId = await InsertPayment(cartId);

                if (paymentId.HasValue)
                {
                    // Insert Payment Details
                    bool isPaymentDetailsInserted = await InsertPaymentDetails(paymentId.Value);

                    if (isPaymentDetailsInserted)
                    {
                        TempData["CheckOutSuccessMessage"] = "Thanh toán thành công";

                        // Reset giỏ hàng sau khi thanh toán thành công
                        HttpContext.Session.Set(MyConst.CART_KEY, new List<CartItem>());
                    }
                    else
                    {
                        TempData["CheckOutErrorMessage"] = "Thanh toán thất bại - Lỗi trong quá trình lưu chi tiết thanh toán";
                    }
                }
                else
                {
                    TempData["CheckOutErrorMessage"] = "Thanh toán thất bại - Lỗi trong quá trình lưu thanh toán";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["CheckOutErrorMessage"] = $"Lỗi hệ thống: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        private async Task<int> InsertCart(int customerId)
        {
            var firstItem = Cart.First();

            var cart = new Cart
            {
                CustomerId = customerId,
                ProductId = firstItem.IdProduct, // do thiết kế cart của bạn
                Quantity = firstItem.Quantity,
                CreateAt = DateTime.Now
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return cart.Id;
        }


        // Phương thức để insert Payment vào cơ sở dữ liệu
        private async Task<int?> InsertPayment(int cartId)
        {
            var payment = new Payment
            {
                CartId = cartId,
                CreateAt = DateTime.Now,
                Total = Cart.Sum(item => item.Total)
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return payment.Id;
        }

        // Phương thức để insert Payment Details vào cơ sở dữ liệu
        private async Task<bool> InsertPaymentDetails(int paymentId)
        {
            foreach (var item in Cart)
            {
                var paymentDetail = new PaymentDetail
                {
                    PaymentId = paymentId,
                    ProductId = item.IdProduct,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Total = item.Total,
                    CreateAt = DateTime.Now
                };

                _context.PaymentDetails.Add(paymentDetail);
            }

            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        // Lấy tổng tiền thanh toán
        public IActionResult GetTotalAmount()
        {
            int totalAmount = Cart.Sum(item => item.Total);
            return Json(totalAmount);
        }

        // Làm mới giỏ hàng
        public IActionResult RefreshCartViewComponent()
        {
            return ViewComponent("Cart");
        }

        // Lấy tổng tiền của sản phẩm theo id
        public IActionResult GetTotalProduct(int idProduct)
        {
            var product = Cart.SingleOrDefault(item => item.IdProduct == idProduct);
            int totalAmount = product?.Total ?? 0;
            return Json(totalAmount);
        }
    }
}
