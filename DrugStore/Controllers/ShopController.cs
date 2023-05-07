﻿using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using X.PagedList;
using X.PagedList.Mvc;
using Microsoft.AspNetCore.Identity;
using DrugStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using System.Drawing.Printing;
using Newtonsoft.Json;

namespace DrugStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly DrugStoreDbContext dbContext =  new DrugStoreDbContext().Created();
        List<GioHang> gioHangs;
        List<CT_HoaDon> cT_HoaDons;
        private HoaDon hoaDon;
        private readonly IHttpContextAccessor contx;
        private UserManager<AppNetUser> userManager;
        private SignInManager<AppNetUser> signInManager;

        public ShopController(UserManager<AppNetUser> userManager, SignInManager<AppNetUser> signInManager, IHttpContextAccessor contx)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.contx = contx;
            //var user = userManager.Users.ToList();
        }
        public IActionResult Index(int? page)
        {
            if (page == null) { page = 1;}
            page  = page < 1 ? 1 : page;
            int pageSize = 3;
            return View(dbContext.SanPhams.OrderBy(s => s.TenSP).ToPagedList((int)page, pageSize));
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
            TakeShopingCart(userManager.GetUserId(User));
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
            TakeShopingCart(userManager.GetUserId(User));
            double result = 0;
            if (gioHangs.Count == 0)
            {
                return result;
            }
            foreach (var item in gioHangs)
            {
                SanPham sanPham = dbContext.SanPhams.Find(item.MaSP);
                //if (doiTuongKD.GiamGia == 0)
                //{
                //    result = (double)((int)item.SoLuong * (doiTuongKD.DonGia)) + result;
                //    continue;
                //}
                result = (double)((int)item.SoLuong * (sanPham.DonGia - (sanPham.DonGia * sanPham.GiamGia/100))) + result;
            }
            return result;
        }

        [Authorize]
        public ActionResult AddToCart(Guid id,string strURL)
        {
            TakeShopingCart(userManager.GetUserId(User));
            GioHang spGioHang = gioHangs.FirstOrDefault(n => n.MaSP == id);
            if (spGioHang != null)
            {
                spGioHang.SoLuong++;
                spGioHang.ThanhTien = spGioHang.ThanhTien + (spGioHang.SanPham.DonGia - (spGioHang.SanPham.DonGia*spGioHang.SanPham.GiamGia/100));
                dbContext.GioHangs.Update(spGioHang);
                dbContext.SaveChanges();
                TakeShopingCart(userManager.GetUserId(User));
                
            }
            else if (spGioHang == null)
            {
                SanPham sp = dbContext.SanPhams.FirstOrDefault(n => n.MaSP == id);
                spGioHang = new GioHang();
                spGioHang.MaSP = id;
                spGioHang.Id = userManager.GetUserId(User);
                spGioHang.SoLuong = 1;
                spGioHang.ThanhTien = (sp.DonGia - (sp.DonGia * sp.GiamGia / 100))*spGioHang.SoLuong;
                dbContext.GioHangs.Add(spGioHang);
                dbContext.SaveChanges();
                TakeShopingCart(userManager.GetUserId(User));

            }
            return Redirect(strURL);

        }

        [Authorize]
        public ActionResult DeleteFromCart(Guid id)
        {
            TakeShopingCart(userManager.GetUserId(User));
            GioHang sanPham = gioHangs.FirstOrDefault(n => n.MaSP == id);
            if (sanPham != null)
            {
                dbContext.GioHangs.Remove(sanPham);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Shopingcart");
        }

        [Authorize]
        public IActionResult Shopingcart(int page = 1)
        {
            TakeShopingCart(userManager.GetUserId(User));
            page = page < 1 ? 1 : page;
            int pageSize = 3;
            ViewBag.CountCart = CountCart();
            ViewBag.SumPriceCart = SumPriceCart();
            return View(gioHangs.ToPagedList((int)page, (int)pageSize));
        }
        public IActionResult Pay()
        {
            TakeBill();
            cT_HoaDons = TakeListProductIsBougth();
            if (cT_HoaDons != null)
            {
                ViewBag.ListProducIsBought = cT_HoaDons;
            }
            return View();
        }

        public void LoginPay(HoaDon hoaDon)
        {
            if (signInManager.IsSignedIn(User))
            {
                hoaDon.Id = userManager.GetUserId(User);
                AspNetUser aspNetUser = dbContext.AspNetUsers.Find(userManager.GetUserId(User));
                hoaDon.TenNguoiMua = aspNetUser.FirstName + aspNetUser.LastName;
                hoaDon.Email = aspNetUser.Email;
                hoaDon.SDT = aspNetUser.PhoneNumber;
            }
            else
            {
                hoaDon.Id = null;
                hoaDon.TenNguoiMua = null;
                hoaDon.Email = null;
                hoaDon.SDT = null;
            }
        }

        public void TakeBill()
        {
            if (hoaDon == null) 
            { 
                hoaDon = new HoaDon();
                hoaDon.SoDH = Guid.NewGuid();
                hoaDon.NgayLap = DateTime.Now;
                hoaDon.DaThanhToan = false;
            }
            LoginPay(hoaDon);
            //string hoaDonString = JsonConvert.SerializeObject(hoaDon);
            //contx.HttpContext.Session.SetString("", hoaDonString);
        }

        public List<CT_HoaDon> TakeListProductIsBougth()
        {
            //TakeBill();
            string dsSpMuaString = contx.HttpContext.Session.GetString("dsSpMua");

            if (dsSpMuaString == null)
            {
                cT_HoaDons = new List<CT_HoaDon>();
                contx.HttpContext.Session.SetString("dsSpMua", JsonConvert.SerializeObject(cT_HoaDons));
            }
            else
            {
                List<CT_HoaDon>? cT_HoaDons = JsonConvert.DeserializeObject<List<CT_HoaDon>>(dsSpMuaString);

            }
            return cT_HoaDons;
        } 

        public ActionResult AddProductIsBought(Guid idSP)
        {
            if (idSP == null)
            {
                return RedirectToAction("Index");
            }
            SanPham sp = dbContext.SanPhams.Find(idSP);
            cT_HoaDons = TakeListProductIsBougth();

            if (sp != null)
            {
                CT_HoaDon spDuocMua = cT_HoaDons.FirstOrDefault(c => c.MaSP == idSP);
                if (spDuocMua == null)
                {
                    spDuocMua = new CT_HoaDon();
                    spDuocMua.MaSP = sp.MaSP;
                    spDuocMua.SoLuong = 1;
                    spDuocMua.ThanhTien = (sp.DonGia - (sp.DonGia * sp.GiamGia / 100)) * spDuocMua.SoLuong;
                    cT_HoaDons.Add(spDuocMua);
                }
                else
                {
                    spDuocMua.SoLuong++;
                    spDuocMua.ThanhTien = spDuocMua.ThanhTien + (sp.DonGia - (sp.DonGia * sp.GiamGia / 100));
                }
               
            }
            return RedirectToAction("Pay", "Shop");

        }
    }
}
