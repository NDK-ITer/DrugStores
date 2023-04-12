using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Entities
{
    public class GioHang
    {
        public string Id { get; set; }
        public string MaSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
