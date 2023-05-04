using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiThuocAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        // GET: LoaiThuocAdmin
        public ActionResult Index(List<LoaiThuoc> loaiThuocs)
        {
            ViewBag.DsLoaiThuoc = dbContext.LoaiThuocs.ToList();
            return View();
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
        public ActionResult Create(LoaiThuoc loaiThuocInput)
        {
            try
            {
                LoaiThuoc loaiThuoc = new LoaiThuoc();
                loaiThuoc.TenLoaiThuoc = loaiThuocInput.TenLoaiThuoc;
                dbContext.LoaiThuocs.Add(loaiThuoc);
                dbContext.SaveChanges();    
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
            LoaiThuoc loaiThuoc = dbContext.LoaiThuocs.Find(id);
            return View(loaiThuoc);
        }

        // POST: LoaiThuocAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoaiThuoc loaiThuoc)
        {
            try
            {
                dbContext.Entry(loaiThuoc).State = EntityState.Modified;
                dbContext.SaveChanges();
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
            LoaiThuoc loaiThuoc = dbContext.LoaiThuocs.Find(id);
            return View(loaiThuoc);
        }

        // POST: LoaiThuocAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                LoaiThuoc loaiThuoc = dbContext.LoaiThuocs.Find(id);
                dbContext.LoaiThuocs.Remove(loaiThuoc);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
