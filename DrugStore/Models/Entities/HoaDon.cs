using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{
    

    public partial class HoaDon
    {
        public HoaDon()
        {
            CT_HoaDon = new HashSet<CT_HoaDon>();
        }

        public Guid SoDH { get; set; }
        public string Id { get; set; }

        public DateTime? NgayLap { get; set; }

        public decimal? TongThanhTien { get; set; }

        public string TenNguoiMua { get; set; }

        public string Email { get; set; }

        public string SDT { get; set; }

        public string DiaChiGiao { get; set; }

        public DateTime? NgayGiao { get; set; }

        public string GhiChu { get; set; }

        public Guid? MaHT { get; set; }

        public virtual ICollection<CT_HoaDon> CT_HoaDon { get; set; }

        public virtual HinhThucThanhToan HinhThucThanhToan { get; set; }
    }
}
