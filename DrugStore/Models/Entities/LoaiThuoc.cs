using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Models.Entities
{
    [Table("LoaiThuoc")]
    public class LoaiThuoc
    {
        public LoaiThuoc()
        {
            Thuocs = new HashSet<ThuocInput>();
        }
        [Key]
        [DisplayName("Mã Loại Thuốc")]
        public int MaLT { get; set; }
        [StringLength(100)]
        [DisplayName("Tên Loại Thuốc")]
        public string? TenLoaiThuoc { get; set; }
        [DisplayName("Các Loại Thuốc")]
        public virtual ICollection<ThuocInput> Thuocs { get; set; }
    }
}
