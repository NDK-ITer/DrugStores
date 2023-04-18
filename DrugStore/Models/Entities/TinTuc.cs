
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DrugStore.Models.Entities
{
    public partial class TinTuc
    {
        public Guid MaTT { get; set; }

        public string AnhDaiDien { get; set; }

        public DateTime? ThoiGiaDang { get; set; }

        public string NoiDung { get; set; }

        public string MoTaTT { get; set; }

        public int? SoLuotXem { get; set; }
    }
}
