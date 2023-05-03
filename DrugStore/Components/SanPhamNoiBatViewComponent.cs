using DrugStore.Models.Entities;
using DrugStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Components
{
    public class SanPhamNoiBatViewComponent:ViewComponent
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<SanPham> sanPhamNoibats = dbContext.SanPhams.OrderBy(s => s.SoLanMua).ToList();
            return View(sanPhamNoibats);
        }
    }
}
