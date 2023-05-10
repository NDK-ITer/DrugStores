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
            Thuocs = new HashSet<Thuoc>();
        }
        [Key]
        [ForeignKey("TongHopLoaiSP")]
        [DisplayName("Mã Loại Thuốc")]
        public Guid MaLT { get; set; }
        [StringLength(100)]
        [DisplayName("Tên Loại Thuốc")]
        public string? TenLoaiThuoc { get; set; }
        [DisplayName("Các Loại Thuốc")]
        public virtual ICollection<Thuoc> Thuocs { get; set; }
        public TongHopLoaiSP? TongHopLoaiSP { get; set; }
    }
}
