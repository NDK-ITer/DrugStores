using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Entities
{
    public class DanhMuc
    {
        public DanhMuc()
        {
            this.SanPhams = new HashSet<SanPham>();
        }
        public string MaDM { get; set; }
        public string TenDM { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
