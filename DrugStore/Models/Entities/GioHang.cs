using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{

    [Table("GioHang")]
    public partial class GioHang
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(400)]
        [DisplayName("Mã Người Dùng")]
        public string Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DisplayName("Mã Sản Phẩm")]
        public Guid MaSP { get; set; }
        [DisplayName("Số Lượng")]
        public int? SoLuong { get; set; }
        [DisplayName("Thành Tiền")]
        public decimal? ThanhTien { get; set; }

        public virtual SanPhamInput SanPham { get; set; }
    }
}
