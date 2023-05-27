using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Components
{
    public class SanPhamMoiViewComponent : ViewComponent
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(dbContext.SanPhams.OrderByDescending(c => c.NgayTao).Take(4).ToList());
        }
    }
}
