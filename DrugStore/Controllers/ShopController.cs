using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using X.PagedList;
using X.PagedList.Mvc;
using Microsoft.AspNetCore.Identity;
using DrugStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using System.Drawing.Printing;

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

        public void TakeShopingCart(string idUser)
        {
            
            gioHangs = dbContext.GioHangs.Where(s => s.Id == idUser).ToList();
            if (gioHangs == null)
            {
                gioHangs = new List<GioHang>();
            }
        }

        private int CountCart()
        {
            TakeShopingCart(_userManager.GetUserId(User));
            int result = 0;
            if (gioHangs.Count == 0)
            {
                return result;
            }
            foreach (var item in gioHangs)
            {
                result = result + (int)item.SoLuong;
            }
            return result;
        }

        private double SumPriceCart()
        {
            TakeShopingCart(_userManager.GetUserId(User));
            double result = 0;
            if (gioHangs.Count == 0)
            {
                return result;
            }
            foreach (var item in gioHangs)
            {
                SanPham doiTuongKD = dbContext.SanPhams.Find(item.MaSP);
                //if (doiTuongKD.GiamGia == 0)
                //{
                //    result = (double)((int)item.SoLuong * (doiTuongKD.DonGia)) + result;
                //    continue;
                //}
                result = (double)((int)item.SoLuong * (doiTuongKD.DonGia - (doiTuongKD.DonGia * doiTuongKD.GiamGia))) + result;
            }
            return result;
        }

        [Authorize]
        public ActionResult AddToCart(Guid id)
        {
            TakeShopingCart(_userManager.GetUserId(User));
            GioHang spGioHang = gioHangs.FirstOrDefault(n => n.MaSP == id);
            if (spGioHang != null)
            {
                spGioHang.SoLuong++;
                dbContext.GioHangs.Update(spGioHang);
                dbContext.SaveChanges();
                TakeShopingCart(_userManager.GetUserId(User));
                
            }
            else if (spGioHang == null)
            {
                spGioHang = new GioHang();
                spGioHang.MaSP = id;
                spGioHang.Id = _userManager.GetUserId(User);
                spGioHang.SoLuong = 1;
                dbContext.GioHangs.Add(spGioHang);
                dbContext.SaveChanges();
                TakeShopingCart(_userManager.GetUserId(User));

            }
            return RedirectToAction("Index");

        }

        [Authorize]
        public IActionResult Shopingcart(int page = 1)
        {
            TakeShopingCart(_userManager.GetUserId(User));
            page = page < 1 ? 1 : page;
            int pageSize = 3;
            ViewBag.CountCart = CountCart();
            ViewBag.SumPriceCart = SumPriceCart();
            return View(gioHangs.ToPagedList((int)page, (int)pageSize));
        }
        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
