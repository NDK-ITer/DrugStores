using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;

namespace DrugStore.Components
{
    public class SanPhamLienQuanViewComponent : ViewComponent
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        public async Task<IViewComponentResult> InvokeAsync(Guid maLSP, int? maHSX)
        {
            List<SanPham> sanPhamLienQuans = dbContext.SanPhams.Where(s => s.Thuoc.MaLT == maLSP).ToList();
            sanPhamLienQuans.AddRange(dbContext.SanPhams.Where(c => c.MaHSX ==  maHSX).ToList());
            return View(sanPhamLienQuans);
        }
    }
}
