using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Models.Entities
{
    [Table("LoaiThuoc")]
    public class LoaiThuoc
    {
        public LoaiThuoc()
        {
            Thuocs = new HashSet<Thuoc>();
        }
        [Key]
        
        public int MaLT { get; set; }
        [StringLength(100)]
        public string? TenLoaiThuoc { get; set; }
        public virtual ICollection<Thuoc> Thuocs { get; set; }
    }
}
