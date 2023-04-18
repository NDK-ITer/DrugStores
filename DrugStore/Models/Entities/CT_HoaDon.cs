using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{
    public partial class CT_HoaDon
    {
        [Key]
        [Column(Order = 0)]
        public Guid SoDH { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid MaSP { get; set; }

        public int? SoLuong { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
