using DrugStore.Models;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class SanPhamsAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();


        public async Task<IActionResult> Index(
     string sortOrder,
     string currentFilter,
     string searchString,
     int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
           
          
           

            ViewData["CurrentFilter"] = searchString;

            var sanPhams = from s in dbContext.SanPhams 
                           select s;

           

            if (!String.IsNullOrEmpty(searchString))
            {
                sanPhams = sanPhams.Where(s => s.TenSP.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sanPhams = sanPhams.OrderByDescending(s => s.TenSP);
                    break;
                case "Date":
                    sanPhams = sanPhams.OrderBy(s => s.NgayTao);
                    break;
                case "date_desc":
                    sanPhams = sanPhams.OrderByDescending(s => s.NgayTao);
                    break;
                default:
                    sanPhams = sanPhams.OrderBy(s => s.TenSP);
                    break;
            }
            var list = sanPhams.Include(s => s.HangSX).Include(s=>s.TrangThai).Include(s=>s.LoaiSP).Include(s => s.Thuoc);
            int pageSize = 3;
            return View(await PaginatedList<SanPham>.CreateAsync(list.AsNoTracking(), pageNumber ?? 1, pageSize));

           
           
        }

        public ActionResult Create()
        {
            //ViewBag.MaLoaiSP = new SelectList(dbContext.LoaiSPs, "MaLoaiSP", "TenLoaiSP");
            //ViewBag.MaHSX = new SelectList(dbContext.HangSXes, "MaHSX", "TenHSX");
            //ViewBag.MaTT = new SelectList(dbContext.TrangThais, "MaTT", "TenTT");
            List<LoaiSP> loaiSPs = dbContext.LoaiSPs.ToList();
            return View(loaiSPs);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(LoaiSP loaiSP)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        string fullPath = Path.Combine(
        //            Directory.GetCurrentDirectory(),
        //            "wwwroot", "MyFiles",
        //            myfiles.FileName
        //            );

        //        using (var file = new FileStream(fullPath, FileMode.Create))
        //        {
        //            myfile.CopyTo(file);
        //        }


        //    }
        //    return View();
        //}

        public ActionResult CreateType(string id)
        {
            if (id == "T")
            {
                return RedirectToAction("Create", "ThuocAdmin");
            }
            return RedirectToAction("Create");
        }

    
    }
}
