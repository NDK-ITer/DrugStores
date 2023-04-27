using DrugStore.Areas.Admin.Models;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThuocAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        private readonly IWebHostEnvironment environment;
        private readonly string fileImagePath = "Images/SanPham/Thuoc";

        public ThuocAdminController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        // GET: ThuocAdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ThuocAdminController/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: ThuocAdminController/Create
        public ActionResult Create()
        {
            ViewBag.MaHSX = new SelectList(dbContext.HangSXes, "MaHSX", "TenHSX");
            //ViewBag.MaLoaiSP = new SelectList(dbContext.LoaiSPs, "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaTT = new SelectList(dbContext.TrangThais, "MaTT", "TenTT");
            ViewBag.MaLT = new SelectList(dbContext.LoaiThuocs, "MaLT", "TenLoaiThuoc");
            return View();
        }

        // POST: ThuocAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThuocInput thuocInput, IFormFile fileImage)
        {
            try
            {
                SanPham sanPham = new SanPham();
                sanPham.MaSP = Guid.NewGuid();
                Thuoc thuoc = new Thuoc();

                if (fileImage != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(fileImage.FileName);
                    string extention = Path.GetExtension(fileImage.FileName);
                    fileName = sanPham.MaSP.ToString() + extention;
                    string uploadFolder = Path.Combine(environment.WebRootPath, fileImagePath);
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        fileImage.CopyTo(stream);
                    }
                    thuocInput.AnhDaiDien = fileName;
                }

                if (ModelState.IsValid)
                {
                    
                    sanPham.TenSP = thuocInput.TenSP;
                    sanPham.CongDung = thuocInput.CongDung;
                    sanPham.MoTa = thuocInput.MoTa;
                    sanPham.AnhDaiDien = thuocInput.AnhDaiDien;
                    sanPham.SoLanMua = 0;
                    sanPham.MaLoaiSP = thuocInput.MaLoaiSP;
                    sanPham.MaTT = thuocInput.MaTT;
                    sanPham.GiamGia = thuocInput.GiamGia;
                    sanPham.DonGia = thuocInput.DonGia;
                    sanPham.MaHSX = thuocInput.MaHSX;
                    sanPham.SoLuong = thuocInput.SoLuong;

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
            return View();
        }

        // POST: ThuocAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
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
        public ActionResult DeleteConfirmed(Guid id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
