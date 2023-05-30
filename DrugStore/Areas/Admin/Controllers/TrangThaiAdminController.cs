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
    public class TrangThaiAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        // GET: TrangThaiAdmin
        public ActionResult Index()
        {
            return View(dbContext.TrangThais.ToList());
        }

        // GET: TrangThaiAdmin/Details/5
        public ActionResult Details(int id)
        {
            TrangThai trangThai = dbContext.TrangThais.Find(id);
            return View(trangThai);
        }

        // GET: TrangThaiAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrangThaiAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrangThai trangThai)
        {
            try
            {
                dbContext.TrangThais.Add(trangThai);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrangThaiAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            TrangThai trangThai = dbContext.TrangThais.Find(id);
            return View(trangThai);
        }

        // POST: TrangThaiAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TrangThai trangThai)
        {
            try
            {
                dbContext.Entry(trangThai).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrangThaiAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            TrangThai trangThai = dbContext.TrangThais.Find(id);
            return View(trangThai);
        }

        // POST: TrangThaiAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                TrangThai trangThai = dbContext.TrangThais.Find(id);
                dbContext.TrangThais.Remove(trangThai);
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
