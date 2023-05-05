using DrugStore.Models.Entities;

namespace DrugStore.Models.ViewModel
{
    public class DoanhMucSanPhamViewModel
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        public List<LoaiSP> loaiSPs { get; set; }
        public List<LoaiThuoc> loaiThuocs { get; set; }
        public DoanhMucSanPhamViewModel() 
        {
            loaiSPs = dbContext.LoaiSPs.ToList();
            loaiThuocs = dbContext.LoaiThuocs.ToList();
        }

    }
}
