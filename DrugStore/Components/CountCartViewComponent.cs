using DrugStore.Areas.Identity.Data;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;using Microsoft.AspNetCore.Authorization;


namespace DrugStore.Components
{
    public class CountCartViewComponent: ViewComponent
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            List<GioHang> gioHangs = dbContext.GioHangs.Where(s => s.Id == id).ToList();
            ViewBag.CountCart = gioHangs.Count();
            return View();
        }
    }
}
