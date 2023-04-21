
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{
    [Table("Thuoc")]
    public partial class Thuoc
    {
        [Key]
        public Guid MaSP { get; set; }

        public string? DonViTinh { get; set; }

        public string? LieuDung { get; set; }

        public string? TacDungPhu { get; set; }

        public string? ThanhPhan { get; set; }
        [ForeignKey("LoaiThuoc")]
        public int? MaLT { get; set; }

        public virtual SanPham? SanPham { get; set; }
        public virtual LoaiThuoc? LoaiThuoc { get; set;}
    }
}
