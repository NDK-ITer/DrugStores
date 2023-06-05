using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DrugStore.Models.Entities
{
    [Table("HoaDon")]
    public partial class HoaDon
    {
        public HoaDon()
        {
            CT_HoaDon = new HashSet<CT_HoaDon>();
        }

        [Key]
        [DisplayName("Mã Hóa Đơn")]
        public Guid SoDH { get; set; }

       
        [StringLength(400)]
        [DisplayName("Mã Người Dùng")]
        public string? Id { get; set; }

        [DisplayName("Ngày Lập")]
        public DateTime? NgayLap { get; set; }
        [DisplayName("Tổng Thành Tiền")]
        public decimal? TongThanhTien { get; set; }
        [Required(ErrorMessage = "bắt buộc")]
        [DisplayName("Tên Người Mua")]
        public string? TenNguoiMua { get; set; }
        [Required(ErrorMessage = "bắt buộc")]
        [DisplayName("Địa Chỉ Email")]
        public string? Email { get; set; }
        [DisplayName("Số Điện Thoại Người Mua")]
        [Required(ErrorMessage = "bắt buộc")]
        public string? SDT { get; set; }
        [Required(ErrorMessage = "bắt buộc")]
        [DisplayName("Địa Chỉ Giao")]
        public string? DiaChiGiao { get; set; }
        [Required(ErrorMessage = "bắt buộc")]
        [CheckDateRange]
        [DisplayName("Ngày Giao")]
        public DateTime? NgayGiao { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Ghi Chú")]
        public string? GhiChu { get; set; }

        public int? MaHT { get; set; }

        public virtual ICollection<CT_HoaDon> CT_HoaDon { get; set; }
        [DisplayName("Hình Thức Thanh Toán")]
        public virtual HinhThucThanhToan? HinhThucThanhToan { get; set; }

        public bool? DaThanhToan { get; set; }

        public AspNetUser? User { get; set; }


        public class CheckDateRangeAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value!=null)
                {
                    DateTime dt = (DateTime)value;
                    if (dt >= DateTime.UtcNow.AddDays(7))
                    {
                        return ValidationResult.Success;
                    }

                    return new ValidationResult(ErrorMessage ?? "ngày nhận phải lớn hơn 7 ngày mua ");
                }
                return ValidationResult.Success;
            }

        }
    }
}
