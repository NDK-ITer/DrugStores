using DrugStore.Models.Entities;

namespace DrugStore.Models.ViewModel
{
    public class TrangChuViewModel
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        public List<SanPham> dsSanPhamNoiBat { get; set; }
        public List<TinTuc> tinTucs { get; set; }
        public List<LoaiSP> loaiSPs { get; set; }
        public List<LoaiThuoc> loaiThuocs { get; set; }

        public TrangChuViewModel()
        {
            loaiThuocs = dbContext.LoaiThuocs.ToList();
            loaiSPs = dbContext.LoaiSPs.ToList();
            dsSanPhamNoiBat = dbContext.SanPhams.ToList();
        }
    }
}
