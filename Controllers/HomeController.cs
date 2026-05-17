using WebBanSanPhamCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebBanSanPhamCongNghe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Route("/404")]
        public IActionResult PageNotfound()
        {
            return View();
        }

        [Route("/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
