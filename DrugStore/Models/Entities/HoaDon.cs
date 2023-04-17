using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Models.Entities
{
    public class HoaDon
    {
        public Guid SoDH { get; set; }
        public Guid Id { get; set; }
        public Nullable<System.DateTime> NgayLap { get; set; }
        public Nullable<decimal> TongThanhTien { get; set; }
        public string TenNguoiMua { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public virtual List<CT_HoaDon> CTHoaDon { get; set; }
        public virtual AppUser AppUser { get; set; }

    }
}
