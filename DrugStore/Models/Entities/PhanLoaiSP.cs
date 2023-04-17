using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{
    public class PhanLoaiSP
    {
        public Guid MaLoaiSP { get; set; }
        public string TenLoaiSP { get; set; }
        public virtual List<SanPham> SanPhams { get; set; }
    }
}
