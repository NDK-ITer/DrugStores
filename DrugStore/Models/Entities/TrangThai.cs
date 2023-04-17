using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{
    public class TrangThai
    {
        public Guid MaTT { get; set; }
        public string TenTT { get; set; }
        public virtual List<SanPham> SanPhams { get; set; }

    }
}
