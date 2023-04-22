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
        [DisplayName("Số Hóa Đơn")]
        public Guid SoDH { get; set; }

        [Key]
        [Column(Order = 1)]
        [DisplayName("Mã Sản Phẩm")]
        public Guid MaSP { get; set; }
        [DisplayName("Số Lượng")]
        public int? SoLuong { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual SanPhamInput SanPham { get; set; }
    }
}
