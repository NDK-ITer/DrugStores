using DrugStore.Areas.Identity.Data;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TinTucsAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        private readonly IWebHostEnvironment environment;
        private readonly string fileImagePath = "Images/TinTuc/";
        //private readonly IHttpContextAccessor contx;
        //private UserManager<AppNetUser> userManager;
        //private SignInManager<AppNetUser> signInManager;
        //public TinTucsAdminController(UserManager<AppNetUser> userManager, SignInManager<AppNetUser> signInManager, IHttpContextAccessor contx)
        //{
        //    this.userManager = userManager;
        //    this.signInManager = signInManager;
        //    this.contx = contx;
        //    //var user = userManager.Users.ToList();
        //}
        // GET: TinTucs

        public TinTucsAdminController(IWebHostEnvironment environment)
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
                Random random = new Random();
                string fileName = Path.GetFileNameWithoutExtension(fileImage.FileName);
                string extention = Path.GetExtension(fileImage.FileName);
                fileName = tinTuc.MaTT.ToString() + random.Next().ToString()+ extention;
                string uploadFolder = Path.Combine(environment.WebRootPath, fileImagePath);
                var filePath = Path.Combine(uploadFolder, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    fileImage.CopyTo(stream);
                }

                tinTuc.AnhDaiDien = fileName;
                tinTuc.SoLuotXem = 0;
                tinTuc.ThoiGiaDang = DateTime.Now;
                //tinTuc.IdNguoiDang = userManager.GetUserId(User);
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
                    Random random = new Random();
                    string filePathDelete = environment.WebRootPath + "/" + fileImagePath + tinTuc.AnhDaiDien;
                    FileInfo fileDelete = new FileInfo(filePathDelete);
                    fileDelete.Delete();

                    string fileName = Path.GetFileNameWithoutExtension(fileImage.FileName);
                    string extention = Path.GetExtension(fileImage.FileName);
                    fileName = tinTuc.MaTT.ToString() + random.Next().ToString() + extention;
                    tinTuc.AnhDaiDien = fileName;
                    string uploadFolder = Path.Combine(environment.WebRootPath, fileImagePath);
                    var filePath = Path.Combine(uploadFolder, fileName);

                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        fileImage.CopyTo(stream);
                    }
                }

                //dbContext.Entry(tinTuc).State = EntityState.Modified;
                dbContext.TinTucs.Update(tinTuc);
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
                string filePathDelete = environment.WebRootPath + "/" + fileImagePath + tinTuc.AnhDaiDien;
                FileInfo fileDelete = new FileInfo(filePathDelete);
                fileDelete.Delete();
                dbContext.TinTucs.Remove(tinTuc);
                dbContext.SaveChanges();
                fileDelete.Delete();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
