using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        public int MaTT { get; set; }

        public string TenTT { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
