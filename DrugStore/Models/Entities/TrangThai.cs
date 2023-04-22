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
    [Table("TrangThai")]
    public partial class TrangThai
    {
        public TrangThai()
        {
            SanPhams = new HashSet<SanPham>();
        }
        [Key]
        [DisplayName("Mã Trạng Thái")]
        public int MaTT { get; set; }
        [DisplayName("Tên Trạng Thái")]
        public string TenTT { get; set; }
        [DisplayName("Các Sản Phẩm")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
