using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{
    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            CT_HoaDon = new HashSet<CT_HoaDon>();
            GioHangs = new HashSet<GioHang>();
        }

        [Key]
        public Guid MaSP { get; set; }

        public string TenSP { get; set; }

        public string CongDung { get; set; }

        public string MoTa { get; set; }

        public string AnhDaiDien { get; set; }

        public int? SoLanMua { get; set; }

        public Guid? MaLoaiSP { get; set; }

        public Guid? MaDM { get; set; }

        public Guid? MaTT { get; set; }

        public decimal? GiamGia { get; set; }

        public decimal? DonGia { get; set; }

        public Guid? MaHSX { get; set; }

        public int? SoLuong { get; set; }

        public string DSAnhSP { get; set; }

        public virtual ICollection<CT_HoaDon> CT_HoaDon { get; set; }

        public virtual ICollection<GioHang> GioHangs { get; set; }

        public virtual HangSX HangSX { get; set; }

        public virtual LoaiSP LoaiSP { get; set; }

        public virtual TrangThai TrangThai { get; set; }

        public virtual Thuoc Thuoc { get; set; }
    }
}
