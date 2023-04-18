using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{
    

    public partial class LoaiSP
    {
        public LoaiSP()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public Guid MaLoaiSP { get; set; }
        public string TenLoaiSP { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
