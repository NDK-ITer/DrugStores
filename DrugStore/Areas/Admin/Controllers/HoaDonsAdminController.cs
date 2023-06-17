using DrugStore.Areas.Identity.Data;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using X.PagedList;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class HoaDonsAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        private UserManager<AppNetUser> userManager;
        private SignInManager<AppNetUser> signInManager;
        private readonly IHttpContextAccessor contx;

        public HoaDonsAdminController(UserManager<AppNetUser> userManager, SignInManager<AppNetUser> signInManager, IHttpContextAccessor contx)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.contx = contx;
            //var user = userManager.Users.ToList();
        }
        // GET: HoaDonsController
        public IActionResult Index(int? page, string? keySearch,bool? thanhtoan)
        {
            List<HoaDon> hoaDons;
            if (thanhtoan == null)
            {
                if (!keySearch.IsNullOrEmpty())
                {
                    hoaDons = dbContext.HoaDons.Where(c => c.HinhThucThanhToan.TenHT.Contains(keySearch) || c.TenNguoiMua.Contains(keySearch) || c.NgayLap.ToString().Contains(keySearch) || c.NgayLap.ToString().Contains(keySearch)).OrderBy(c => c.NgayLap).ToList();
                }
                else
                {
                    hoaDons = dbContext.HoaDons.OrderBy(s => s.NgayLap).ToList();
                }
            }
            else
            {
                if (thanhtoan == true)
                {
                    if (!keySearch.IsNullOrEmpty())
                    {
                        hoaDons = dbContext.HoaDons.Where(c => c.HinhThucThanhToan.TenHT.Contains(keySearch) || c.TenNguoiMua.Contains(keySearch) || c.NgayLap.ToString().Contains(keySearch) || c.NgayLap.ToString().Contains(keySearch) && c.DaThanhToan==true).OrderBy(c => c.NgayLap).ToList();
                    }
                    else
                    {
                        hoaDons = dbContext.HoaDons.Where(c=>c.DaThanhToan==true).OrderBy(s => s.NgayLap).ToList();
                    }
                }
                else
                {
                    if (!keySearch.IsNullOrEmpty())
                    {
                        hoaDons = dbContext.HoaDons.Where(c => c.HinhThucThanhToan.TenHT.Contains(keySearch) || c.TenNguoiMua.Contains(keySearch) || c.NgayLap.ToString().Contains(keySearch) || c.NgayLap.ToString().Contains(keySearch)&& c.DaThanhToan == false).OrderBy(c => c.NgayLap).ToList();
                    }
                    else
                    {
                        hoaDons = dbContext.HoaDons.Where(c => c.DaThanhToan == false).OrderBy(s => s.NgayLap).ToList();
                    }
                }
            }
          
            
            if (page == null) { page = 1; }
            page = page < 1 ? 1 : page;
            int pageSize = 5;
            return View(hoaDons.ToPagedList((int)page, pageSize));
        }
        public IActionResult Details(Guid SoDH)
        {
            HoaDon hoaDon = dbContext.HoaDons.Find(SoDH);
            return View(hoaDon);
        }
        public IActionResult Edit(Guid SoDH)
        {
            HoaDon hoaDon = dbContext.HoaDons.Find(SoDH);
            return View(hoaDon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HoaDon hoaDon)
        {
            try
            {
                HoaDon hoaDon1 = dbContext.HoaDons.Find(hoaDon.SoDH);
                hoaDon1.DaThanhToan=hoaDon.DaThanhToan;
                hoaDon1.DiaChiGiao = hoaDon.DiaChiGiao;
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
