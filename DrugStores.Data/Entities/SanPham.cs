using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Entities
{
    public class SanPham
    {
        public Guid MaSP { get; set; }
        public string TenSP { get; set; }
        public string CongDung { get; set; }
        public string MoTa { get; set; }
        public string AnhDaiDien { get; set; }
        public Nullable<int> SoLanMua { get; set; }
        public Guid MaLoaiSP { get; set; }
        public Guid MaDM { get; set; }
        public Guid MaTT { get; set; }
        public Guid MaHSX { get; set; }
        public decimal GiamGia { get; set; }
        public decimal DonGia { get; set; }
        public virtual List<CT_HoaDon> CTHoaDon { get; set; }
        public virtual DanhMuc DanhMuc { get; set; }
        public virtual List<GioHang> GioHangs { get; set; }
        public virtual PhanLoaiSP PhanLoaiSP { get; set; }
        public virtual TrangThai TrangThai { get; set; }
        public virtual HangSX HangSX { get; set; }
    }
}
