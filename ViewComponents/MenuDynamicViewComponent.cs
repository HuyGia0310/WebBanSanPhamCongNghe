using WebBanSanPhamCongNghe.Areas.Admin.Data;
using WebBanSanPhamCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebBanSanPhamCongNghe.ViewComponents
{
    public class MenuDynamicViewComponent : ViewComponent
    {
        private readonly MyShopContext _context;

        public MenuDynamicViewComponent(MyShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            // Lấy tất cả các menu đã được sắp xếp theo MenuIndex
            var listMenu = await _context.Menus.OrderBy(m => m.MenuIndex).ToListAsync();

            //Truy cập Role của Customer đang đăng nhâp 
            string RoleCustomer = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            // Sử dụng danh sách tạm thời để lưu các mục cần xóa
            var itemsToRemove = new List<Menu>();

            // Nếu không được phân quyền để truy cập trang Admin
            // Và đường dẫn Url của Menu chứa Area Admin
            foreach (var item in listMenu)
            {
                if ((RoleCustomer != "Administrator" && item.MenuUrl != null && item.MenuUrl.Contains("Admin")) || !(bool)item.IsVisible)
                {
                    itemsToRemove.Add(item);
                }
            }

            // Xóa các phần tử không hợp lệ
            foreach (var item in itemsToRemove)
            {
                listMenu.Remove(item);
            }

            // Lấy Menu Cha
            var navBar = listMenu
                .Where(p => p.ParentId == null)
                .Select(p => new NavbarItem
                {
                    Id = p.Id,
                    ParentId = p.ParentId,
                    Title = p.Title,
                    MenuUrl = p.MenuUrl,
                    MenuIndex = (int)p.MenuIndex,
                    IsVisible = (bool)p.IsVisible,
                    // Lấy Menu Con
                    subItems = listMenu.Where(s => s.ParentId == p.Id)
                        .Select(s => new NavbarItem
                        {
                            Id = s.Id,
                            ParentId = s.ParentId,
                            Title = s.Title,
                            MenuUrl = s.MenuUrl,
                            MenuIndex = (int)s.MenuIndex,
                            IsVisible = (bool)s.IsVisible,
                            subItems = null
                        })
                        .ToList()

                }).ToList();

            return View("Default", navBar);
        }
    }
}
