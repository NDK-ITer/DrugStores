using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Components
{
    public class LoaiThuocViewComponent: ViewComponent
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(dbContext.LoaiThuocs.OrderByDescending(c => c.TenLoaiThuoc).ToList());
        }
    }
}
