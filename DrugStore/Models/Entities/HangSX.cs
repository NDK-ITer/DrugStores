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
    [Table("HangSX")]
    public partial class HangSX
    {
        public HangSX()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [DisplayName("Mã Hãng Sản Xuất")]
        public int MaHSX { get; set; }
        [DisplayName("Tên Hãng Sản Xuất")]
        public string TenHSX { get; set; }
        [DisplayName("Địa Chỉ")]
        public string DiaChiNSX { get; set; }

        [StringLength(50)]
        [DisplayName("Số Điện Thoại Hãng")]
        public string SDT { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
