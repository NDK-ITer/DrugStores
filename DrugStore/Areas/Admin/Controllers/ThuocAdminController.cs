using DrugStore.Areas.Admin.Data;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
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
            SanPham sanPham = dbContext.SanPhams.Find(id);
            return View(sanPham);
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
        public ActionResult Create(ThuocInput thuocInput, List<IFormFile> fileImages, IFormFile fileImage)
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
                else
                {
                    return RedirectToAction(nameof(Create));

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
        public ActionResult Edit(Thuoc thuocUpdate, List<IFormFile> fileImages, IFormFile fileImage)
        {
            try
            {
                Random random = new Random();
                if (fileImage != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(fileImage.FileName);
                    string extention = Path.GetExtension(fileImage.FileName);
                    fileName = thuocUpdate.SanPham.MaSP.ToString() + random.Next().ToString() + extention;
                    string uploadFolder = Path.Combine(environment.WebRootPath, fileImagePath);
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        fileImage.CopyTo(stream);
                    }
                thuocUpdate.SanPham.AnhDaiDien = fileName;
                }
                if (fileImages != null)
                {
                    string Listimage = "";
                    foreach (var image in fileImages)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                        string extention = Path.GetExtension(image.FileName);
                        fileName = thuocUpdate.SanPham.MaSP.ToString() + random.Next().ToString() + extention;
                        string uploadFolder = Path.Combine(environment.WebRootPath, fileImagePath);
                        var filePath = Path.Combine(uploadFolder, fileName);
                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        {
                            image.CopyTo(stream);
                        }
                        Listimage += fileName + ",";
                    }
                    thuocUpdate.SanPham.DSAnhSP = Listimage;
                }
                var thuoc = dbContext.Thuocs.Find(thuocUpdate.MaSP);
                if (thuoc != null)
                {
                    thuoc.SanPham.TenSP = thuocUpdate.SanPham.TenSP;
                    thuoc.SanPham.CongDung = thuocUpdate.SanPham.CongDung;
                    thuoc.SanPham.MoTa = thuocUpdate.SanPham.MoTa;
                    thuoc.SanPham.AnhDaiDien = thuoc.SanPham.AnhDaiDien;
                    thuoc.SanPham.DSAnhSP = thuocUpdate.SanPham.DSAnhSP;
                    thuoc.SanPham.MaTT = thuocUpdate.SanPham.MaTT;
                    thuoc.SanPham.GiamGia = thuocUpdate.SanPham.GiamGia;
                    thuoc.SanPham.DonGia = thuocUpdate.SanPham.DonGia;
                    thuoc.SanPham.NgayCapNhat = DateTime.Now;
                    thuoc.TacDungPhu = thuocUpdate.TacDungPhu;
                    thuoc.MaLT = thuocUpdate.MaLT;
                }
                dbContext.Thuocs.Update(thuoc);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: ThuocAdminController/Delete/5
        public ActionResult Delete(Guid id)
        {
            try
            {
                SanPham sanPham = dbContext.SanPhams.Find(id);
                Thuoc thuoc = dbContext.Thuocs.Find(id);
                if (sanPham != null && thuoc!=null)
                {
                    if (!string.IsNullOrEmpty(sanPham.AnhDaiDien))
                    {
                        string filePathDelete = environment.WebRootPath + "/" + fileImagePath + "/" + sanPham.AnhDaiDien;
                        FileInfo fileDelete = new FileInfo(filePathDelete);
                        fileDelete.Delete();

                    }


                    if (!string.IsNullOrEmpty(sanPham.DSAnhSP))
                    {
                        string[] filesold = sanPham.DSAnhSP.Split(',');
                        foreach (var fileold in filesold)
                        {
                            if (!string.IsNullOrEmpty(fileold))
                            {
                                string filePathDelete1 = environment.WebRootPath + "/" + fileImagePath + "/" + fileold;
                                FileInfo fileDelete1 = new FileInfo(filePathDelete1);
                                fileDelete1.Delete();
                            }
                            
                        }
                    }
                    dbContext.Thuocs.Remove(thuoc);
                    dbContext.SaveChanges();
                    dbContext.SanPhams.Remove(sanPham);
                    dbContext.SaveChanges();

                }

                return RedirectToAction("Index", "SanPhamsAdmin");
            }
            catch
            {
                return View();
            }
        }
    }
}
