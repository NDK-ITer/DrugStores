using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Models.Entities
{
    public class LoaiThuoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("MaLT")]
        public int MaLT { get; set; }
        [Column("TenLoaiThuoc", TypeName = "nvarchar")]
        [MaxLength(100)]
        public string? TenLoaiThuoc { get; set; }
        public virtual ICollection<Thuoc>? Thuocs { get; set; }
    }
}
