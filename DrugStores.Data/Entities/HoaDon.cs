using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStores.Data.Entities
{
    public class HoaDon
    {
        public HoaDon()
        {
            this.CT_HoaDon = new HashSet<CT_HoaDon>();
        }

        public string SoDH { get; set; }
        public string Id { get; set; }
        public Nullable<System.DateTime> NgayLap { get; set; }
        public Nullable<decimal> TongThanhTien { get; set; }
        public string TenNguoiMua { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public virtual ICollection<CT_HoaDon> CT_HoaDon { get; set; }
    }
}
