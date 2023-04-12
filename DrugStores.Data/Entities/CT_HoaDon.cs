using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Entities
{
    public partial class CT_HoaDon
    {
        public Guid SoDH { get; set; }
        public Guid MaSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public HoaDon HoaDon { get; set; }
        public SanPham SanPham { get; set; }
    }
}
