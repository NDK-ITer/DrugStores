
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace DrugStore.Models.Entities
{
    [Table("TinTuc")]
    public partial class TinTuc
    {
        [Key]
        public Guid? MaTT { get; set; }
        [DisplayName("Ảnh Đại Diện")]
        public string? AnhDaiDien { get; set; }
        [DisplayName("Thời Gian Đăng")]
        public DateTime? ThoiGiaDang { get; set; }
        [Column(TypeName = "ntext")]
        [DisplayName("Nội Dung")]
        public string? NoiDung { get; set; }
        [DisplayName("Mô Tả Tin Tức")]
        public string? MoTaTT { get; set; }
        [DisplayName("Số Lượt Xem")]
        public int? SoLuotXem { get; set; }
        [ForeignKey("Users")]
        public string? IdNguoiDang { get; set; }
        public virtual AspNetUser? Users { get; set; }
        public virtual ICollection<TagTinTuc>? TagTinTucs { get; set; }
        public List<Tag> Tags { get; } = new();
    }
}
