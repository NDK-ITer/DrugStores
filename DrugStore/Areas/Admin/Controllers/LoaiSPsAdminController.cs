using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LoaiSPsAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        // GET: LoaiSPsAdmin
        public ActionResult Index()
        {
            List<LoaiSP> loaiSPs = dbContext.LoaiSPs.ToList();
            return View(loaiSPs);
        }

        // GET: LoaiSPsAdmin/Details/5
        public ActionResult Details(string id)
        {
            LoaiSP loaiSP = dbContext.LoaiSPs.Find(id);
            return View(loaiSP);
        }

        // GET: LoaiSPsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiSPsAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoaiSP loaiSPInput)
        {
            try
            {
                LoaiSP loaiSP = new LoaiSP();
                loaiSP.MaLoaiSP = loaiSPInput.MaLoaiSP;
                loaiSP.TenLoaiSP = loaiSPInput.TenLoaiSP;
                dbContext.LoaiSPs.Add(loaiSP);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoaiSPsAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            LoaiSP loaiSP = dbContext.LoaiSPs.Find(id);
            return View(loaiSP);
        }

        // POST: LoaiSPsAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoaiSP loaiSP)
        {
            try
            {
                dbContext.Entry(loaiSP).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoaiSPsAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            LoaiSP loaiSP = dbContext.LoaiSPs.Find(id);
            return View(loaiSP);
        }

        // POST: LoaiSPsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                LoaiSP loaiSP = dbContext.LoaiSPs.Find(id);
                dbContext.LoaiSPs.Remove(loaiSP);
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
