using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class HinhThucThanhToansController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        // GET: HinhThucThanhToans
        public ActionResult Index()
        {
            return View(dbContext.HinhThucThanhToans.ToList());
        }

        // GET: HinhThucThanhToans/Details/5
        public ActionResult Details(int id)
        {
            HinhThucThanhToan hinhThucThanhToan = dbContext.HinhThucThanhToans.Find(id);
            return View(hinhThucThanhToan);
        }

        // GET: HinhThucThanhToans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HinhThucThanhToans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HinhThucThanhToan hinhThucThanhToan)
        {
            try
            {
                dbContext.HinhThucThanhToans.Add(hinhThucThanhToan);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HinhThucThanhToans/Edit/5
        public ActionResult Edit(int id)
        {
            HinhThucThanhToan hinhThucThanhToan = dbContext.HinhThucThanhToans.Find(id);
            return View(hinhThucThanhToan);
        }

        // POST: HinhThucThanhToans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HinhThucThanhToan hinhThucThanhToan)
        {
            try
            {
                dbContext.Entry(hinhThucThanhToan).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HinhThucThanhToans/Delete/5
        public ActionResult Delete(int id)
        {
            HinhThucThanhToan hinhThucThanhToan = dbContext.HinhThucThanhToans.Find(id);
            return View(hinhThucThanhToan);
        }

        // POST: HinhThucThanhToans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                HinhThucThanhToan hinhThucThanhToan = dbContext.HinhThucThanhToans.Find(id);
                dbContext.HinhThucThanhToans.Remove(hinhThucThanhToan);
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
