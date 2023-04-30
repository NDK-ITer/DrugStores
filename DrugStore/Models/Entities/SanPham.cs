using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DrugStore.Models.Entities
{
    [Table("SanPham")]
    public partial class SanPham
    {
        public SanPham()
        {
            CT_HoaDon = new HashSet<CT_HoaDon>();
            GioHangs = new HashSet<GioHang>();
        }

        [Key]
        [ForeignKey("Thuoc")]
        [DisplayName("Mã Sản Phẩm")]
        public Guid MaSP { get; set; }
        [DisplayName("Tên Sản Phẩm")]
        public string? TenSP { get; set; }
        [DisplayName("Công Dụng")]
        public string? CongDung { get; set; }
        [DisplayName("Mô tả")]
        public string? MoTa { get; set; }
        [DisplayName("Ảnh Đại Diện")]
        public string? AnhDaiDien { get; set; }
        [DisplayName("Số Lần Mua")]
        public int? SoLanMua { get; set; }

        [ForeignKey("LoaiSP")]
        public string? MaLoaiSP { get; set; }
        
        [ForeignKey("TrangThai")]
        public int? MaTT { get; set; }
        [ForeignKey("HangSX")]
        public int? MaHSX { get; set; }
        [DisplayName("Giảm Giá")]
        public decimal? GiamGia { get; set; }
        [DisplayName("Đơn Giá")]
        public decimal? DonGia { get; set; }
        [DisplayName("Số Lượng")]
        public int? SoLuong { get; set; }
        [DisplayName("Danh Sách Ảnh")]
        public string? DSAnhSP { get; set; }

        [DisplayName("Ngày Tạo")]
        public DateTime? NgayTao {get; set; }

        [DisplayName("Người Tạo")]
        public string? NguoiTao { get;set; }

        [DisplayName("Ngày Cập Nhật")]
        public DateTime? NgayCapNhat { get; set; }

        [DisplayName("Người Cập Nhật")]
        public string? NguoiCapNhat { get; set; }


        public virtual ICollection<CT_HoaDon> CT_HoaDon { get; set; }

        public virtual ICollection<GioHang> GioHangs { get; set; }
        [DisplayName("Hãng Sản Xuất")]
        public virtual HangSX HangSX { get; set; }
        [DisplayName("Loại Sản Phẩm")]
        public virtual LoaiSP LoaiSP { get; set; }
        [DisplayName("Trạng Thái")]
        public virtual TrangThai TrangThai { get; set; }
        public virtual Thuoc Thuoc { get; set; }
    }
}
