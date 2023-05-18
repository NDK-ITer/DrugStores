using DrugStore.Areas.Identity.Data;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public IActionResult Index(int? page, string? keySearch)
        {
            List<HoaDon> hoaDons;
            if (keySearch != null)
            {
                hoaDons = dbContext.HoaDons.Where(c=>c.HinhThucThanhToan.TenHT.Contains(keySearch) || c.TenNguoiMua.Contains(keySearch) || c.NgayLap.ToString().Contains(keySearch) || c.NgayLap.ToString().Contains(keySearch)).OrderBy(c => c.NgayLap).ToList();
            }
            else
            {
                hoaDons = dbContext.HoaDons.OrderBy(s => s.NgayLap).ToList();
            }
            
            if (page == null) { page = 1; }
            page = page < 1 ? 1 : page;
            int pageSize = 5;
            return View(hoaDons.ToPagedList((int)page, pageSize));
        }
        public IActionResult Details(Guid SoDH)
        {
            return View();
        }

    }
}
