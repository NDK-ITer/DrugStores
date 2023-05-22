using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using X.PagedList;

namespace DrugStore.Controllers
{
    public class AutoConsultingController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();

        public IActionResult Index(int? page, List<SanPham>? sanPhams)
        {
            if (sanPhams == null || sanPhams.Count <= 0)
            {
                sanPhams = dbContext.SanPhams.Where(s => s.MaTT == 1).OrderBy(s => s.TenSP).ToList();
            }
            if (page == null) { page = 1; }
            page = page < 1 ? 1 : page;
            int pageSize = 6;
            return View(sanPhams.ToPagedList((int)page, pageSize));
        }
        public IActionResult Consulting()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Consulting(string consultString)
        {
            if (consultString.IsNullOrEmpty())
            {
                return View();
            }
            string[] temp = consultString.Trim().ToUpper().Split(' ');
            List<string> worldList = temp.ToList();
            var dsSP = dbContext.SanPhams.ToList();
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
            
            return RedirectToAction("Index", dsSP);
        }
    }
}
