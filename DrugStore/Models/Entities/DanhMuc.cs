using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{
    public class DanhMuc
    {
        public Guid MaDM { get; set; }
        public string TenDM { get; set; }
        public virtual List<SanPham> SanPhams { get; set; }
    }
}
