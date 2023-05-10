﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        [ForeignKey("CT_HoaDon")]
        [DisplayName("Mã Hóa Đơn")]
        public Guid SoDH { get; set; }

        [Required]
        [ForeignKey("User")]
        [StringLength(400)]
        [DisplayName("Mã Người Dùng")]
        public string? Id { get; set; }

        [DisplayName("Ngày Lập")]
        public DateTime? NgayLap { get; set; }
        [DisplayName("Tổng Thành Tiền")]
        public decimal? TongThanhTien { get; set; }
        [DisplayName("Tên Người Mua")]
        public string? TenNguoiMua { get; set; }
        [DisplayName("Địa Chỉ Email")]
        public string? Email { get; set; }
        [DisplayName("Số Điện Thoại Người Mua")]
        public string? SDT { get; set; }
        [DisplayName("Địa Chỉ Giao")]
        public string? DiaChiGiao { get; set; }
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
    }
}
