using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Entities
{
    public class GioHang
    {
        public Guid Id { get; set; }
        public Guid MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
        public virtual SanPham SanPham { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
