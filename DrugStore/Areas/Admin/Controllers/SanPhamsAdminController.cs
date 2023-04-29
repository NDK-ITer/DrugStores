using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamsAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        public ActionResult Index()
        {
            List<SanPham> sanPhams = dbContext.SanPhams.Include(s => s.HangSX).Include(s => s.LoaiSP).Include(s => s.TrangThai).ToList();

            return View(sanPhams);
        }
    }
}
