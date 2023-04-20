using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{
    [Table("LoaiSP")]
    public partial class LoaiSP
    {
        public LoaiSP()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        public string MaLoaiSP { get; set; }

        public string TenLoaiSP { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
