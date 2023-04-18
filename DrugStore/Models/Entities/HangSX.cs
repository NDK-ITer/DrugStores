using System;
using System.Collections.Generic;
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
        public Guid MaHSX { get; set; }

        public string TenHSX { get; set; }

        public string DiaChiNSX { get; set; }

        [StringLength(50)]
        public string SDT { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
