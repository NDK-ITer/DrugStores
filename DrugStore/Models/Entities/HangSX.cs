using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{
    

    public partial class HangSX
    {
        public HangSX()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public Guid MaHSX { get; set; }

        public string TenHSX { get; set; }

        public string DiaChiNSX { get; set; }

        public string SDT { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
