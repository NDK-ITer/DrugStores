using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using X.PagedList;
using X.PagedList.Mvc;
using Microsoft.AspNetCore.Identity;
using DrugStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace DrugStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly DrugStoreDbContext dbContext =  new DrugStoreDbContext().Created();
        List<GioHang> gioHangs;

        private UserManager<AppNetUser> _userManager;
        public ShopController(UserManager<AppNetUser> userManager)
        {
            _userManager = userManager;
            //var user = userManager.Users.ToList();
        }
        public IActionResult Index(int page = 1)
        {
            page  = page < 1 ? 1 : page;
            int pageSize = 3;
            return View(dbContext.SanPhams.OrderBy(s => s.TenSP).ToPagedList(page, pageSize));
        }
        public IActionResult Product(Guid id)
        {
            SanPham sanPham = dbContext.SanPhams.Find(id);
            return View(sanPham);
        }

        public void TakeShopingcart(string idUser)
        {
            
            gioHangs = dbContext.GioHangs.Where(s => s.Id == idUser).ToList();
            if (gioHangs == null)
            {
                gioHangs = new List<GioHang>();
            }
        }

        [Authorize]
        public IActionResult Shopingcart()
        {
            TakeShopingcart(_userManager.GetUserId(User));
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
