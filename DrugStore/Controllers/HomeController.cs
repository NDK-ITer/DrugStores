using DrugStore.Models;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DrugStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DrugStoreDbContext dbContext = new DrugStoreDbContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<LoaiThuoc> loaiThuocs = dbContext.LoaiThuocs.ToList();
            return View(loaiThuocs);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}