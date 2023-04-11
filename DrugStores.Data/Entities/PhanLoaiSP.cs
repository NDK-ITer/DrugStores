using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Entities
{
    public class PhanLoaiSP
    {
        public PhanLoaiSP()
        {
            this.SanPhams = new HashSet<SanPham>();
        }

        public string MaLoaiSP { get; set; }
        public string TenLoaiSP { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
