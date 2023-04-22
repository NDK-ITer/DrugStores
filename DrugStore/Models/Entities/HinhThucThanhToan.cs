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
    [Table("HinhThucThanhToan")]
    public partial class HinhThucThanhToan
    {
        public HinhThucThanhToan()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        [DisplayName("Mã Hình Thức")]
        public int MaHT { get; set; }

        [StringLength(50)]
        [DisplayName("Tên Hình Thức")]
        public string TenHT { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
