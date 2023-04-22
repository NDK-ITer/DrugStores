using DrugStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
