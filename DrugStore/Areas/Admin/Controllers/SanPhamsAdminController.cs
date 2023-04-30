using DrugStore.Models;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Drawing.Imaging;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamsAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();


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

            var sanPhams = from s in dbContext.SanPhams select s;

           

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
            var list = sanPhams.Include(s => s.HangSX).Include(s=>s.TrangThai).Include(s=>s.LoaiSP);
            int pageSize = 3;
            return View(await PaginatedList<SanPham>.CreateAsync(list.AsNoTracking(), pageNumber ?? 1, pageSize));

           
           
        }

        public ActionResult Create()
        {
            ViewBag.MaLoaiSP = new SelectList(dbContext.LoaiSPs, "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaHSX = new SelectList(dbContext.HangSXes, "MaHSX", "TenHSX");
            ViewBag.MaTT = new SelectList(dbContext.TrangThais, "MaTT", "TenTT");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SanPham sanPham ,List<IFormFile> myfiles,IFormFile myfile)
        {
            //if (ModelState.IsValid)
            //{
            //    string fullPath = Path.Combine(
            //        Directory.GetCurrentDirectory(),
            //        "wwwroot", "MyFiles",
            //        myfiles.FileName
            //        );

            //    using (var file = new FileStream(fullPath, FileMode.Create))
            //    {
            //        myfile.CopyTo(file);
            //    }

               
            //}
            return View();
        }

        public static void Compress(Bitmap srcBitMap, string destFile, long level)
        {
            Stream s = new FileStream(destFile, FileMode.Create); //create FileStream,this will finally be used to create the new image 
            Compress(srcBitMap, s, level);  //main progress to compress image
            s.Close();
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        private static void Compress(Bitmap srcBitmap, Stream destStream, long level)
        {
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            myEncoder = Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, level);
            myEncoderParameters.Param[0] = myEncoderParameter;
            srcBitmap.Save(destStream, myImageCodecInfo, myEncoderParameters);
        }

    }
}
