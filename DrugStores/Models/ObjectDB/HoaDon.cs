using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStores.Models.ObjectDB
{
    public class HoaDon
    {
        public string SoHD { get; set; }
        public string Id { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongThanhTien { get; set; }
        public string TenNguoiMua { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
    }
}
