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
    public partial class CT_HoaDon
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("HoaDon")]
        [DisplayName("Số Hóa Đơn")]
        public Guid? SoDH { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("SanPham")]
        [DisplayName("Mã Sản Phẩm")]
        public Guid MaSP { get; set; }
        [DisplayName("Số Lượng")]
        public int? SoLuong { get; set; }
        [DisplayName("Thành Tiền")]
        public decimal? ThanhTien { get; set; }
        public virtual HoaDon? HoaDon { get; set; }
        public virtual SanPham? SanPham { get; set; }
    }
}
