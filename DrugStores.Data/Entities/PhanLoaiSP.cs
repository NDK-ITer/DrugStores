using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Entities
{
    public class PhanLoaiSP
    {
        public string MaLoaiSP { get; set; }
        public string TenLoaiSP { get; set; }
        public List<SanPham> SanPhams { get; set; }
    }
}
