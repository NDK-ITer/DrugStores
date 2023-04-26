using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThuocAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        private readonly IWebHostEnvironment environment;
        private readonly string fileImagePath = "Images/SanPham/Thuoc";

        public ThuocAdminController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        // GET: ThuocAdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ThuocAdminController/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: ThuocAdminController/Create
        public ActionResult Create()
        {
            ViewBag.MaHSX = new SelectList(dbContext.HangSXes, "MaHSX", "TenHSX");
            //ViewBag.MaLoaiSP = new SelectList(dbContext.LoaiSPs, "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaTT = new SelectList(dbContext.TrangThais, "MaTT", "TenTT");
            ViewBag.MaLT = new SelectList(dbContext.LoaiThuocs, "MaLT", "TenLoaiThuoc");
            return View();
        }

        // POST: ThuocAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThuocInput thuocInput, IFormFile fileImage)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ThuocAdminController/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View();
        }

        // POST: ThuocAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ThuocAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ThuocAdminController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
