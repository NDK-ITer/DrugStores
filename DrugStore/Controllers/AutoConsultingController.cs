using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Controllers
{
    public class AutoConsultingController : Controller
    {
        public IActionResult Consulting()
        {
            return View();
        }
    }
}
