
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
    public partial class ThuocInput : SanPhamInput
    {
        [DisplayName("Đơn Vị Tính")]
        public string? DonViTinh { get; set; }
        [DisplayName("Liều Dùng")]
        public string? LieuDung { get; set; }
        [DisplayName("Tác Dụng Phụ")]
        public string? TacDungPhu { get; set; }
        [DisplayName("Thành Phần")]
        public string? ThanhPhan { get; set; }
        [ForeignKey("LoaiThuoc")]
        [DisplayName("Loại Thuốc")]
        public int? MaLT { get; set; }
    }
}
