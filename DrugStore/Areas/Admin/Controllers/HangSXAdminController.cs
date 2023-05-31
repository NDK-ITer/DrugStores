using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class HangSXAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        // GET: HangSXesAdmin
        public ActionResult Index()
        {
            List<HangSX> hangSXes = dbContext.HangSXes.ToList();
            return View(hangSXes);
        }

        // GET: HangSXesAdmin/Details/5
        public ActionResult Details(int id)
        {
            HangSX hangSX = dbContext.HangSXes.Find(id);
            return View(hangSX);
        }

        // GET: HangSXesAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HangSXesAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HangSX hangSX)
        {
            try
            {
                dbContext.HangSXes.Add(hangSX);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HangSXesAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            HangSX hangSX = dbContext.HangSXes.Find(id);
            return View(hangSX);
        }

        // POST: HangSXesAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HangSX hangSX)
        {
            try
            {
                dbContext.Entry(hangSX).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HangSXesAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            HangSX hangSX = dbContext.HangSXes.Find(id);
            return View(hangSX);
        }

        // POST: HangSXesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                HangSX hangSX = dbContext.HangSXes.Find(id);
                dbContext.HangSXes.Remove(hangSX);
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
