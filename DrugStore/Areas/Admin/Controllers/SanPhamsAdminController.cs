using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Areas.Admin.Controllers
{
    public class SanPhamsAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
