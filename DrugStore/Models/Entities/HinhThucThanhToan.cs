using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{
    
    public partial class HinhThucThanhToan
    {
        public HinhThucThanhToan()
        {
            HoaDons = new HashSet<HoaDon>();
        }
        public Guid MaHT { get; set; }
        public string TenHT { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
