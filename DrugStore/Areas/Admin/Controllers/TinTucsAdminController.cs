using DrugStore.Areas.Admin.Data;
using DrugStore.Areas.Identity.Data;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class TinTucsAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        private readonly IWebHostEnvironment environment;
        private readonly string fileImagePath = "Images/TinTuc/";
        private readonly IHttpContextAccessor contx;
        private UserManager<AppNetUser> userManager;
        private SignInManager<AppNetUser> signInManager;
        public TinTucsAdminController(UserManager<AppNetUser> userManager, SignInManager<AppNetUser> signInManager, IHttpContextAccessor contx, IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.contx = contx;
            this.environment = environment;

            //var user = userManager.Users.ToList();
        }
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
        [Authorize]
        public ActionResult Create()
        {
            var model = new TinTucInput();
           List<string> idtag = new List<string>();
            model.drptag = dbContext.Tags.Select(x => new SelectListItem { Text = x.IdTag ,Value=x.IdTag}).ToList();
            return View(model);
        }

        // POST: TinTucs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TinTucInput TinTucInput, IFormFile fileImage)
        {
            try
            {
               Guid id = Guid.NewGuid();
                Random random = new Random();
                string fileName = Path.GetFileNameWithoutExtension(fileImage.FileName);
                string extention = Path.GetExtension(fileImage.FileName);
                fileName = id.ToString() + random.Next().ToString() + extention;
                string uploadFolder = Path.Combine(environment.WebRootPath, fileImagePath);
                var filePath = Path.Combine(uploadFolder, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    fileImage.CopyTo(stream);
                }
                TinTuc tinTuc = new TinTuc()
                {
                    MaTT=id,
                    MoTaTT=TinTucInput.description,
                    NoiDung=TinTucInput.content,
                    ThoiGiaDang = DateTime.Now,
                    AnhDaiDien=fileName,
                    IdNguoiDang = userManager.GetUserId(User),
                    SoLuotXem=0,


                };
                dbContext.TinTucs.Add(tinTuc);
                foreach (string tag in TinTucInput.idtag){
                    TagTinTuc tagTinTuc = new TagTinTuc()
                    {
                        IdTag = tag,
                        MaTT=id,
                    };

                    dbContext.TagTinTucs.Add(tagTinTuc);
                } ;
                
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
            TinTuc tinTuc=dbContext.TinTucs.Find(id);
            TinTucInput TinTucinput = new TinTucInput()
            {
                idtintuc= (Guid)tinTuc.MaTT,
                createdDate=tinTuc.ThoiGiaDang,
                content=tinTuc.NoiDung,
                description=tinTuc.MoTaTT,
                cover=tinTuc.AnhDaiDien,

            };


            List<string> idtag = new List<string>();




            


            var x = dbContext.TagTinTucs.Where(x => x.MaTT == TinTucinput.idtintuc).Select(x => new SelectListItem { Text = x.IdTag, Value = x.IdTag,Selected=true}).ToList();
            var y = dbContext.Tags.Select(x => new SelectListItem { Text = x.IdTag, Value = x.IdTag }).ToList();

            var z=new List<SelectListItem>();
            foreach(var n in x)
            {
                z.Add(n);
            }
            foreach(var n in y)
            {
                z.Add(n);
            }

            foreach(var n in y)
            {
                foreach (var m in x)
                {if(m.Value.Equals(n.Value) && n.Selected==false)
                    z.Remove(n);
                if(m.Value.Equals(n.Value) && n.Selected == true)
                    { z.Add(m); }
                }
            }

           
            TinTucinput.drptag = z;

            return View(TinTucinput);
        }

        // POST: TinTucs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TinTucInput tinTucInput, IFormFile fileImage)
        {
            try
            {
                if (fileImage != null)
                {
                    Random random = new Random();
                    string filePathDelete = environment.WebRootPath + "/" + fileImagePath + tinTucInput.cover;
                    FileInfo fileDelete = new FileInfo(filePathDelete);
                    fileDelete.Delete();

                    string fileName = Path.GetFileNameWithoutExtension(fileImage.FileName);
                    string extention = Path.GetExtension(fileImage.FileName);
                    fileName =tinTucInput.idtintuc.ToString() + random.Next().ToString() + extention;
                    tinTucInput.cover = fileName;
                    string uploadFolder = Path.Combine(environment.WebRootPath, fileImagePath);
                    var filePath = Path.Combine(uploadFolder, fileName);

                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        fileImage.CopyTo(stream);
                    }
                }
                TinTuc tinTuc= dbContext.TinTucs.Find(tinTucInput.idtintuc);
                tinTuc.MaTT = tinTucInput.idtintuc;
                tinTuc.MoTaTT = tinTucInput.description;
                tinTuc.NoiDung = tinTucInput.content;
                tinTuc.AnhDaiDien = tinTucInput.cover;
                

                
                dbContext.TinTucs.Update(tinTuc);
                var tagTinTuc1 = dbContext.TagTinTucs.Where(x=>x.MaTT==tinTucInput.idtintuc).ToList();
                foreach ( var tag in tagTinTuc1)
                {
                    dbContext.TagTinTucs.Remove(tag);
                }
               
                dbContext.SaveChanges();

                foreach (string tag in tinTucInput.idtag)
                {
                    TagTinTuc tagTinTuc = new TagTinTuc()
                    {
                        IdTag = tag,
                        MaTT = tinTucInput.idtintuc,
                    };

                    dbContext.TagTinTucs.Add(tagTinTuc);
                };
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
