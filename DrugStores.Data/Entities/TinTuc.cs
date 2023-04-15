using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Entities
{
    public class TinTuc
    {
        public Guid MaTT { get; set; }
        public string AnhDaiDien { get; set; }
        public DateTime ThoiGianDang { get; set; }
        public string NoiDung { get; set; }
        public int LuotXem { get; set; }
        public string MoTaTT { get; set; }
    }
}
