using DrugStore.Models.Entities;
using DrugStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Components
{
    public class CaNhanHoaViewComponent : ViewComponent
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        public async Task<IViewComponentResult> InvokeAsync(string idUser, int? page)
        {
            List<SanPham> dsSanPhamDeXua = new List<SanPham>();
            if (idUser != null)
            {
                if (page == null) { page = 1; }
                page = page < 1 ? 1 : page;
                int pageSize = 9;
                List<CT_CaNhanHoa> cT_CaNhanHoas = dbContext.CT_CaNhanHoas.Where(c => c.Id == idUser).OrderBy(c => c.SoLanXem)/*.Take(5)*/.ToList();
                foreach (var item in cT_CaNhanHoas)
                {
                    TongHopLoaiSP newItem = dbContext.TongHopLoaiSPs.Find(item.MaTHLSP);
                    if (newItem != null)
                    {
                        if (newItem.LoaiThuoc != null)
                        {
                            LoaiThuoc loaiThuoc = newItem.LoaiThuoc;
                            foreach (var itemThuoc in loaiThuoc.Thuocs)
                            {
                                dsSanPhamDeXua.Add(itemThuoc.SanPham);
                            }
                        }
                    }
                }
            }
            if (dsSanPhamDeXua.Count < 1)
            {
                dsSanPhamDeXua = dbContext.SanPhams.OrderBy(s => s.NgayTao).ToList();
            }
            return View(dsSanPhamDeXua.Take(12));
        }
    }
}
