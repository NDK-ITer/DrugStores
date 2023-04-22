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
    [Table("LoaiSP")]
    public partial class LoaiSP
    {
        public LoaiSP()
        {
            SanPhams = new HashSet<SanPhamInput>();
        }

        [Key]
        [DisplayName("Mã Loại Sản Phẩm")]
        public string MaLoaiSP { get; set; }
        [DisplayName("Tên Loại Sản Phẩm")]
        public string TenLoaiSP { get; set; }
        [DisplayName("Các Sản Phẩm")]
        public virtual ICollection<SanPhamInput> SanPhams { get; set; }
    }
}
