using DrugStore.Areas.Admin.Data;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThuocAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        private readonly IWebHostEnvironment environment;
        private readonly string fileImagePath = "Images/SanPham/Thuoc";
        public ThuocAdminController(IWebHostEnvironment environment)
        {
            this.environment = environment;
            
        }
        // GET: ThuocAdminController
        public ActionResult Index()
        {
            return RedirectToAction("Index","SanPhamsAdmin");
        }

        // GET: ThuocAdminController/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: ThuocAdminController/Create
        public ActionResult Create()
        {
            //ViewBag.MaLoaiSP = new SelectList(dbContext.LoaiSPs, "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaHSX = new SelectList(dbContext.HangSXes, "MaHSX", "TenHSX");
            ViewBag.MaTT = new SelectList(dbContext.TrangThais, "MaTT", "TenTT");
            ViewBag.MaLT = new SelectList(dbContext.LoaiThuocs, "MaLT", "TenLoaiThuoc");
            return View();
        }

        // POST: ThuocAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThuocInput thuocInput,List<IFormFile> fileImages, IFormFile fileImage)
        {
            try
            {
                SanPham sanPham = new SanPham();
                sanPham.MaSP = Guid.NewGuid();
                Thuoc thuoc = new Thuoc();
                Random random = new Random();

                if (ModelState.IsValid)
                {
                    if (fileImage != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(fileImage.FileName);
                        string extention = Path.GetExtension(fileImage.FileName);
                        fileName = sanPham.MaSP.ToString()+ random.Next().ToString() + extention;
                        string uploadFolder = Path.Combine(environment.WebRootPath, fileImagePath);
                        var filePath = Path.Combine(uploadFolder, fileName);
                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        {
                            fileImage.CopyTo(stream);
                        }
                        thuocInput.AnhDaiDien = fileName;
                    }
                    if(fileImages != null)
                    {
                        string Listimage = "";
                        foreach (var image in fileImages)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                            string extention = Path.GetExtension(image.FileName);
                            fileName = sanPham.MaSP.ToString() + random.Next().ToString() + extention;
                            string uploadFolder = Path.Combine(environment.WebRootPath, fileImagePath);
                            var filePath = Path.Combine(uploadFolder, fileName);
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                image.CopyTo(stream);
                            }
                            Listimage += fileName +",";
                        }
                        thuocInput.DSAnhSP = Listimage;
                    }

                    sanPham.TenSP = thuocInput.TenSP;
                    sanPham.CongDung = thuocInput.CongDung;
                    sanPham.MoTa = thuocInput.MoTa;
                    sanPham.AnhDaiDien = thuocInput.AnhDaiDien;
                    sanPham.DSAnhSP=thuocInput.DSAnhSP;
                    sanPham.SoLanMua = 0;
                    sanPham.MaLoaiSP = thuocInput.MaLoaiSP;
                    sanPham.MaTT = thuocInput.MaTT;
                    sanPham.GiamGia = thuocInput.GiamGia;
                    sanPham.DonGia = thuocInput.DonGia;
                    sanPham.MaHSX = thuocInput.MaHSX;
                    sanPham.SoLuong = thuocInput.SoLuong;
                    sanPham.NgayTao = DateTime.Now;
                    sanPham.MaLoaiSP = "T";

                    thuoc.MaSP = sanPham.MaSP;
                    thuoc.DonViTinh = thuocInput.DonViTinh;
                    thuoc.LieuDung = thuocInput.LieuDung;
                    thuoc.TacDungPhu = thuocInput.TacDungPhu;
                    thuoc.ThanhPhan = thuocInput.ThanhPhan;
                    thuoc.MaLT = thuocInput.MaLT;

                    dbContext.SanPhams.Add(sanPham);
                    dbContext.SaveChanges();
                    dbContext.Thuocs.Add(thuoc);
                    dbContext.SaveChanges();


                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ThuocAdminController/Edit/5
        public ActionResult Edit(Guid id)
        {
            Thuoc thuoc = dbContext.Thuocs.Find(id);
            SanPham sanPham = dbContext.SanPhams.Find(id);
            //LoaiThuoc loaiThuoc = dbContext.LoaiThuocs.Find(thuoc.MaLT);
            ViewBag.MaHSX = new SelectList(dbContext.HangSXes, "MaHSX", "TenHSX", thuoc.SanPham.MaHSX);
            ViewBag.MaTT = new SelectList(dbContext.TrangThais, "MaTT", "TenTT", thuoc.SanPham.MaTT);
            ViewBag.MaLT = new SelectList(dbContext.LoaiThuocs, "MaLT", "TenLoaiThuoc", thuoc.MaLT);
            return View(thuoc);
        }

        // POST: ThuocAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Thuoc thuoc)
        {
            try
            {
                dbContext.Entry(thuoc).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index", "SanPhamAdmin");
            }
            catch
            {
                return View();
            }
        }

        // GET: ThuocAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ThuocAdminController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                Thuoc thuoc = dbContext.Thuocs.Find(id);
                dbContext.Thuocs.Remove(thuoc);
                dbContext.SaveChanges();

                SanPham sanPham = dbContext.SanPhams.Find(id);

                string filePathDelete = environment.WebRootPath + "/" + fileImagePath + sanPham.AnhDaiDien;
                FileInfo fileDelete = new FileInfo(filePathDelete);
                fileDelete.Delete();


                if (!string.IsNullOrEmpty(sanPham.DSAnhSP))
                {
                    string[] filesold = sanPham.DSAnhSP.Split(',');
                    foreach (var fileold in filesold)
                    {
                        string filePathDelete1 = environment.WebRootPath + "/" + fileImagePath + fileold;
                        FileInfo fileDelete1 = new FileInfo(filePathDelete1);
                        fileDelete1.Delete();
                    }
                }

                dbContext.SanPhams.Remove(sanPham);
                dbContext.SaveChanges();
                fileDelete.Delete();

                return RedirectToAction("Index", "SanPhamsAdmin");
            }
            catch
            {
                return View();
            }
        }
    }
}
