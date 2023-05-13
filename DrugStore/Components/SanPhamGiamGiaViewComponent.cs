using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Components
{
    public class SanPhamGiamGiaViewComponent:ViewComponent
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<SanPham> sanPhamGiamGias = dbContext.SanPhams.Where(s => s.GiamGia != 0).ToList();    
            return View(sanPhamGiamGias);
        }
    }
}
