using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Entities
{
    public class HangSX
    {
        public Guid MaHSX { get; set; }
        public string TenHSX { get; set; }
        public List<SanPham> SanPhams { get; set;}
    }
}
