using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DrugStore.Models.Entities
{
    
    public partial class TrangThai
    {
        public TrangThai()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public Guid MaTT { get; set; }

        public string TenTT { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
