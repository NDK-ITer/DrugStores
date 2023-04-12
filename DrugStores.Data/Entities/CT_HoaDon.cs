using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Entities
{
    public partial class CT_HoaDon
    {
        public string SoDH { get; set; }
        public string MaSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public HoaDon HoaDon { get; set; }
        public SanPham SanPham { get; set; }
    }
}
