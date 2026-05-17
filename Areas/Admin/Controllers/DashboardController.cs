using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using WebBanSanPhamCongNghe.Areas.Admin.Data;
using WebBanSanPhamCongNghe.Areas.Admin.Models;

namespace WebBanSanPhamCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class DashboardController : Controller
    {
        private readonly MyShopContext _context;

        public DashboardController(MyShopContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? selectedYear)
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            selectedYear ??= currentYear; // Nếu selectedYear là null, gán giá trị mặc định là năm hiện tại.

            var totalPaymentsThisMonth = _context.Payments
                                                 .Where(p => p.CreateAt.Value.Month == currentMonth && p.CreateAt.Value.Year == currentYear)
                                                 .Count();

            var totalProducts = _context.Products.Count();
            var totalCategories = _context.Categories.Count();
            var totalUsers = _context.Customers.Where(c => c.Role == 0).Count();

            // Lấy danh sách năm có trong hệ thống
            var availableYears = _context.Payments
                                         .Select(p => p.CreateAt.Value.Year)
                                         .Distinct()
                                         .OrderByDescending(y => y)
                                         .ToList();

            // Nếu năm hiện tại không có trong danh sách, thêm nó vào
            if (!availableYears.Contains(currentYear))
            {
                availableYears.Insert(0, currentYear);
            }

            // Tạo SelectList cho AvailableYears
            var yearSelectList = new SelectList(availableYears, selectedYear);

            // Tính số lượng thanh toán và doanh thu theo tháng
            var paymentsPerMonth = new List<int>();
            var revenuePerMonth = new List<int>();

            for (int month = 1; month <= 12; month++)
            {
                var monthlyPayments = _context.Payments
                                              .Where(p => p.CreateAt.Value.Month == month && p.CreateAt.Value.Year == selectedYear)
                                              .Count();
                paymentsPerMonth.Add(monthlyPayments);

                var monthlyRevenue = _context.Payments
                                              .Where(p => p.CreateAt.Value.Month == month && p.CreateAt.Value.Year == selectedYear)
                                              .Sum(p => p.Total ?? 0);
                revenuePerMonth.Add((int)Math.Floor(monthlyRevenue));
            }

            var dashboard = new Dashboard
            {
                SelectedYear = selectedYear,
                AvailableYears = yearSelectList,
                TotalPaymentsThisMonth = totalPaymentsThisMonth,
                TotalProducts = totalProducts,
                TotalCategories = totalCategories,
                TotalUsers = totalUsers,
                PaymentsPerMonth = paymentsPerMonth,
                RevenuePerMonth = revenuePerMonth
            };

            return View(dashboard);
        }
    }
}

