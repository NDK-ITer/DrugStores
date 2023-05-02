using DrugStore.Models.Entities;
using X.PagedList;

namespace DrugStore.Models.ViewModel
{
    public class HomeViewModel
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        public List<SanPham> dsSanPhamNoiBat { get; set; }
        public List<TinTuc> tinTucs { get; set; }
        public List<LoaiSP> loaiSPs { get; set; }
        public List<LoaiThuoc> loaiThuocs { get; set; }

        public HomeViewModel()
        {
            loaiThuocs = dbContext.LoaiThuocs.ToList();
            loaiSPs = dbContext.LoaiSPs.ToList();
            dsSanPhamNoiBat = dbContext.SanPhams.OrderBy(s => s.SoLanMua).ToList();
            tinTucs = dbContext.TinTucs.ToList();
        }
    }
}
