﻿using DrugStore.Areas.Admin.Data;
using DrugStore.Areas.Identity.Data;
using DrugStore.Data;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class HomeAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();

        public IActionResult Index()
        {
            ViewBag.nguoidung=dbContext.AspNetUsers.Count();

          

           return View();
        }

        public ActionResult tong(DateTime datefrom, DateTime dateto)
        {
           var tongtien = dbContext.HoaDons.Where(x => x.NgayLap >= datefrom && x.NgayLap < dateto).Sum(x => x.TongThanhTien);

            var thanhtoan = dbContext.HoaDons.Where(x => x.DaThanhToan == true && x.NgayLap >= datefrom && x.NgayLap < dateto).Sum(x => x.TongThanhTien);

            var chuathanhtoan = dbContext.HoaDons.Where(x => x.DaThanhToan == false && x.NgayLap >= datefrom && x.NgayLap < dateto).Sum(x => x.TongThanhTien);

            return Json(new { tongtien = tongtien, thanhtoan= thanhtoan, chuathanhtoan= chuathanhtoan });
        }

        public ActionResult tongTien(DateTime datefrom, DateTime dateto)
        {
            
         
            List<ChartTongTien> chartTongTiens = new List<ChartTongTien>();
            
            var query = "tongtien @datefrom , @dateto ";

            
            DataTable temp =DataProvider.Instance.ExecuteQuery(query, new object[] { datefrom, dateto });

            foreach (DataRow item in temp.Rows)
            {
                ChartTongTien info = new ChartTongTien(item);

                chartTongTiens.Add(info);
            }
            var data = chartTongTiens;

            return Json(new { data = data});
        }

        public ActionResult tongHoaDon(DateTime datefrom, DateTime dateto)
        {

            
            List<ChartHoaDon> chartHoadons = new List<ChartHoaDon>();

            var query = "tonghoadon @datefrom , @dateto ";


            DataTable temp = DataProvider.Instance.ExecuteQuery(query, new object[] { datefrom, dateto });

            foreach (DataRow item in temp.Rows)
            {
                ChartHoaDon info = new ChartHoaDon(item);

                chartHoadons.Add(info);
            }
            var data = chartHoadons;

            return Json(new { data = data});
        }
    }
}
