using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Entities
{
    public class TrangThai
    {
        public TrangThai()
        {
            this.SanPhams = new HashSet<SanPham>();
        }
        public string MaTT { get; set; }
        public string TenTT { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }

    }
}
