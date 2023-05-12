using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;

namespace DrugStore.Components
{
    public class SanPhamLienQuanViewComponent : ViewComponent
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        public async Task<IViewComponentResult> InvokeAsync(Guid idSP, Guid maLSP, int? maHSX)
        {
            List<SanPham> sanPhamLienQuans = dbContext.SanPhams.Where(s => s.Thuoc.MaLT == maLSP && s.MaSP != idSP).ToList();
            foreach (var item in sanPhamLienQuans)
            {
                SanPham newItem = dbContext.SanPhams.FirstOrDefault(c => c.MaHSX == maHSX);
                if (sanPhamLienQuans.Contains(newItem) == false)
                {
                    sanPhamLienQuans.Add(newItem);
                }
            }
            ViewBag.Count = sanPhamLienQuans.Count;
            return View(sanPhamLienQuans);
        }
    }
}
