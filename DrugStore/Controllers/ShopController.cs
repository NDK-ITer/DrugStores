using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Product()
        {
            return View();
        }

        public IActionResult Shopingcart()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
