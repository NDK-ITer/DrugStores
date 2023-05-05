using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DrugStore.Models.Entities;

namespace DrugStore.Areas.Admin.Data
{
    public partial class SanPhamInput
    {
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

        [DisplayName("Loại Sản Phẩm")]
        public string? MaLoaiSP { get; set; }

        [DisplayName("Trạng Thái")]
        public int? MaTT { get; set; }

        [DisplayName("Hãng Sản Xuất")]
        public int? MaHSX { get; set; }

        [DisplayName("Giảm Giá")]
        public decimal? GiamGia { get; set; }

        [DisplayName("Đơn Giá")]
        public decimal? DonGia { get; set; }

        [DisplayName("Số Lượng")]
        public int? SoLuong { get; set; }

        [DisplayName("Danh Sách Ảnh")]
        public string? DSAnhSP { get; set; }
        public virtual HangSX? HangSX { get; set; }
        [DisplayName("Loại Sản Phẩm")]
        public virtual LoaiSP? LoaiSP { get; set; }
        [DisplayName("Trạng Thái")]
        public virtual TrangThai? TrangThai { get; set; }
    }
}
