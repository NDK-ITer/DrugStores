using DrugStore.Areas.Identity.Data;
using DrugStore.Models;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DrugStore.Controllers
{
    public class BillController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        private readonly UserManager<AppNetUser> userManager;
        private readonly SignInManager<AppNetUser> signInManager;
        public BillController(UserManager<AppNetUser> userManager, SignInManager<AppNetUser> signInManager) 
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [Authorize]
        public IActionResult Index(List<HoaDon> dshd)
        {
           
            return View(dshd);
        }


        public ActionResult ListBill(DateTime datefrom , DateTime dateto)
        {
            List<HoaDon> dshd=new List<HoaDon>();
            if (dshd.IsNullOrEmpty())
            {
                dshd = dbContext.HoaDons.Where(c => c.Id == userManager.GetUserId(User) && c.NgayLap>=datefrom && c.NgayLap<=dateto).OrderByDescending(c => c.NgayLap).ToList();
            }

            List<Bill> list = new List<Bill>();

            foreach (HoaDon d in dshd)
            {
                Bill bill=new Bill();
                bill.id = d.SoDH;
                bill.ngay = (DateTime)d.NgayLap;
                bill.trangthai = (bool)d.DaThanhToan ? "Đã thanh toán" : "chưa thanh toán";
                bill.tongtien = d.TongThanhTien.ToString();
                list.Add(bill);
            }

            return Json(new { list = list });
        }
                 
        public IActionResult Detail(Guid id)
        {
            return View(dbContext.HoaDons.Find(id));
        }
    }
}
