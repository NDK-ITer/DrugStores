using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{
    

    public partial class CT_HoaDon
    {
        
        public Guid SoDH { get; set; }
        
        public Guid MaSP { get; set; }

        public int? SoLuong { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
