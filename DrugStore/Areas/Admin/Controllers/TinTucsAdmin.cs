using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TinTucsAdmin : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        private readonly IWebHostEnvironment environment;
        // GET: TinTucs

        public TinTucsAdmin(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
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
        public ActionResult Create(TinTuc tinTuc, IFormFile fileImage)
        {
            try
            {
                tinTuc.MaTT = Guid.NewGuid();

                string fileName = Path.GetFileNameWithoutExtension(fileImage.FileName);
                string extention = Path.GetExtension(fileImage.FileName);
                fileName = tinTuc.MaTT.ToString() + extention;
                string uploadFolder = Path.Combine(environment.WebRootPath, "Images/TinTuc/");
                var filePath = Path.Combine(uploadFolder, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    fileImage.CopyTo(stream);
                }

                tinTuc.AnhDaiDien = fileName;
                tinTuc.ThoiGiaDang = DateTime.Now;
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
        public ActionResult Edit(TinTuc tinTuc, IFormFile fileImage)
        {
            try
            {
                if (fileImage != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(fileImage.FileName);
                    string extention = Path.GetExtension(fileImage.FileName);
                    fileName = tinTuc.MaTT.ToString() + extention;
                    string uploadFolder = Path.Combine(environment.WebRootPath, "Images/TinTuc/");
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        fileImage.CopyTo(stream);
                    }
                }

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
