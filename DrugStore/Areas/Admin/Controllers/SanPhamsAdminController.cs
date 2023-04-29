using DrugStore.Models;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamsAdminController : Controller
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        public async Task<IActionResult> Index(string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var sanPhams = from s in dbContext.SanPhams select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sanPhams = sanPhams.Where(s => s.TenSP.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sanPhams = sanPhams.OrderByDescending(s => s.TenSP);
                    break;
                case "Date":
                    sanPhams = sanPhams.OrderBy(s => s.NgayTao);
                    break;
                case "date_desc":
                    sanPhams = sanPhams.OrderByDescending(s => s.NgayTao);
                    break;
                default:
                    sanPhams = sanPhams.OrderBy(s => s.TenSP);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<SanPham>.CreateAsync(sanPhams.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
    }
}
