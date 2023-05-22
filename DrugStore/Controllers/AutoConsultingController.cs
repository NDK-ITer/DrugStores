using Azure;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using X.PagedList;

namespace DrugStore.Controllers
{
    public class AutoConsultingController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        
        public IActionResult Consulting(int? page)
        {
            List<SanPham> dsSP = new List<SanPham>(); ;
            if (page == null) { page = 1; }
            page = page < 1 ? 1 : page;
            int pageSize = 6;
            return View(dsSP.ToPagedList((int)page, pageSize));
        }

        [HttpPost]
        public IActionResult Consulting(int? page, string consultString)
        {
            if (consultString.IsNullOrEmpty())
            {
                return View();
            }
            string[] temp = consultString.Trim().ToUpper().Split(' ');
            List<string> worldList = temp.ToList();
            List<SanPham> dsSP = dbContext.SanPhams.ToList();
            foreach (string world in worldList.ToList())
            {
                if (string.IsNullOrEmpty(world))
                {
                    break;
                }
                foreach (var item in dsSP.ToList())
                {
                    if (!item.CongDung.ToUpper().Contains(world))
                    {
                        dsSP.Remove(item);
                    }
                }
                if (dsSP.IsNullOrEmpty())
                {
                    dsSP = dbContext.SanPhams.ToList();
                }
                worldList.Remove(world);
            }


            if (dsSP == null || dsSP.Count <= 0)
            {
                dsSP = new List<SanPham>();
            }
            if (page == null) { page = 1; }
            page = page < 1 ? 1 : page;
            int pageSize = 6;
            ViewBag.Message = "Kết quả của: " + consultString;
            return View(dsSP.ToPagedList((int)page, pageSize));
        }
    }
}
