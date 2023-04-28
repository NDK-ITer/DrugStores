using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamsAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        public ActionResult Index()
        {
            List<SanPham> sanPhams = dbContext.SanPhams.ToList();

            return View(sanPhams);
        }
    }
}
