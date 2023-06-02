using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace DrugStore.Controllers
{
    public class BlogController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        public IActionResult Index(string? search, int? page, string? idTag)
        {
            int pageSize = 6;
            List<TinTuc> dsTinTuc;
            if (search != null)
            {
                dsTinTuc = dbContext.TinTucs.Where(c => c.MoTaTT.Contains(search)).ToList();
                if (dsTinTuc == null)
                {
                    dsTinTuc = new List<TinTuc>();
                }
                if (page == null) { page = 1; }
                page = page < 1 ? 1 : page;
                return View(dsTinTuc.ToPagedList((int)page, pageSize));
            }
            if (idTag != null)
            {
                Tag tag = dbContext.Tags.Find(idTag);
                dsTinTuc = tag.TinTucs;
                if (dsTinTuc == null)
                {
                    dsTinTuc = new List<TinTuc>();
                }
                if (page == null) { page = 1; }
                page = page < 1 ? 1 : page;
                return View(dsTinTuc.ToPagedList((int)page, pageSize));
            }
            dsTinTuc = dbContext.TinTucs.ToList();
            if (page == null) { page = 1; }
            page = page < 1 ? 1 : page;
            return View(dsTinTuc.ToPagedList((int)page, pageSize));
        }

        public IActionResult BlogDetail(Guid id)
        {
            if (id != null)
            {
                TinTuc tinTuc = dbContext.TinTucs.Find(id);
                tinTuc.SoLuotXem++;
                dbContext.SaveChanges();
                dbContext.TinTucs.Update(tinTuc);
                return View(tinTuc);
            }
            return RedirectToAction("Index");
        }
    }
}
