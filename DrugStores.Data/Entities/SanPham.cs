using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Entities
{
    public class SanPham
    {
        public SanPham()
        {
            this.CT_HoaDon = new HashSet<CT_HoaDon>();
            this.GioHangs = new HashSet<GioHang>();
        }

        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string QuyCach { get; set; }
        public string ThanhPhan { get; set; }
        public string CongDung { get; set; }
        public string LieuDung { get; set; }
        public string TacDungPhu { get; set; }
        public string MoTa { get; set; }
        public string AnhDaiDien { get; set; }
        public Nullable<int> SoLanMua { get; set; }
        public string MaLoaiSP { get; set; }
        public string MaDM { get; set; }
        public string MaTT { get; set; }
        public Nullable<decimal> GiamGia { get; set; }
        public virtual ICollection<CT_HoaDon> CT_HoaDon { get; set; }
        public virtual DanhMuc DanhMuc { get; set; }
        public virtual ICollection<GioHang> GioHangs { get; set; }
        public virtual PhanLoaiSP PhanLoaiSP { get; set; }
        public virtual TrangThai TrangThai { get; set; }
    }
}
