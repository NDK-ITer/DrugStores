using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TinTucs : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        // GET: TinTucs
        public ActionResult Index()
        {
            return View(dbContext.TinTucs.ToList());
        }

        // GET: TinTucs/Details/5
        public ActionResult Details(Guid id)
        {
            TinTuc tinTuc = dbContext.TinTucs.Find(id);
            return View(tinTuc);
        }

        // GET: TinTucs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TinTucs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TinTuc tinTuc, IFormFile Anh)
        {
            try
            {
                tinTuc.MaTT = Guid.NewGuid();
                dbContext.TinTucs.Add(tinTuc);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TinTucs/Edit/5
        public ActionResult Edit(Guid id)
        {
            TinTuc tinTuc = dbContext.TinTucs.Find(id);
            return View(tinTuc);
        }

        // POST: TinTucs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TinTuc tinTuc)
        {
            try
            {
                dbContext.Entry(tinTuc).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TinTucs/Delete/5
        public ActionResult Delete(Guid id)
        {
            TinTuc tinTuc = dbContext.TinTucs.Find(id);
            return View(tinTuc);
        }

        // POST: TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                TinTuc tinTuc = dbContext.TinTucs.Find(id);
                dbContext.TinTucs.Remove(tinTuc);
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
