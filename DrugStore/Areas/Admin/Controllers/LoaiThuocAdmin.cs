using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiThuocAdmin : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        // GET: LoaiThuocAdmin
        public ActionResult Index(List<LoaiThuoc> loaiThuocs)
        {
            return View(dbContext.LoaiThuocs.ToList());
        }

        // GET: LoaiThuocAdmin/Details/5
        public ActionResult Details(int? id)
        {
            LoaiThuoc loaiThuoc = dbContext.LoaiThuocs.Find(id);
            return View(loaiThuoc);
        }

        // GET: LoaiThuocAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiThuocAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: LoaiThuocAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoaiThuocAdmin/Edit/5
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

        // GET: LoaiThuocAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoaiThuocAdmin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
