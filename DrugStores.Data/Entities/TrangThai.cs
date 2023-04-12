using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Entities
{
    public class TrangThai
    {
        public string MaTT { get; set; }
        public string TenTT { get; set; }
        public List<SanPham> SanPhams { get; set; }

    }
}
