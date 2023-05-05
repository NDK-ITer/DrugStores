using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Components
{
    public class SumPriceCartViewComponent:ViewComponent
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            List<GioHang> gioHangs = dbContext.GioHangs.Where(s => s.Id == id).ToList();
            ViewBag.SumPriceCart = gioHangs.Sum(c => c.ThanhTien);
            return View();
        }
    }
}
