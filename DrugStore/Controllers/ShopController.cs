using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using X.PagedList;
using X.PagedList.Mvc;



namespace DrugStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        public IActionResult Index(int page = 1)
        {
            page  = page < 1 ? 1 : page;
            int pageSize = 3;
            return View(dbContext.SanPhams.OrderBy(s => s.TenSP).ToPagedList(page, pageSize));
        }
        public IActionResult Product(Guid id)
        {
            SanPham sanPham = dbContext.SanPhams.Find(id);
            LoaiSP loaiSP = dbContext.LoaiSPs.Find(sanPham.MaLoaiSP);
            TrangThai trangThai = dbContext.TrangThais.Find(sanPham.MaTT);
            HangSX hangSX = dbContext.HangSXes.Find(sanPham.MaHSX);
            if (sanPham.MaLoaiSP == "T")
            {
                Thuoc thuoc = dbContext.Thuocs.Find(id) ;
            }
            return View(sanPham);
        }

        public IActionResult Shopingcart()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
