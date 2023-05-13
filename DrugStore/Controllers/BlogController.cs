using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace DrugStore.Controllers
{
    public class BlogController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        public IActionResult Index(string? search, int? page)
        {
            List<TinTuc> dsTinTuc = null;
            if (page == null) { page = 1; }
            page = page < 1 ? 1 : page;
            int pageSize = 6;
            if (search == null)
            {
                dsTinTuc = dbContext.TinTucs.ToList();
            }
            return View(dsTinTuc.ToPagedList((int)page, pageSize));
        }

        public IActionResult BlogDetail()
        {
            return View();
        }
    }
}
