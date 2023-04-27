
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
    [Table("Thuoc")]
    public partial class Thuoc
    {
        [Key]
        [DisplayName("Mã Thuốc")]
        public Guid MaSP { get; set; }
        [DisplayName("Đơn Vị Tính")]
        public string? DonViTinh { get; set; }
        [DisplayName("Liều Dùng")]
        public string? LieuDung { get; set; }
        [DisplayName("Tác Dụng Phụ")]
        public string? TacDungPhu { get; set; }
        [DisplayName("Thành Phần")]
        public string? ThanhPhan { get; set; }
        [ForeignKey("LoaiThuoc")]
        public int? MaLT { get; set; }
        public virtual SanPham SanPham { get; set; }
        [DisplayName("Loại Thuốc")]
        public virtual LoaiThuoc? LoaiThuoc { get; set;}
    }
}
