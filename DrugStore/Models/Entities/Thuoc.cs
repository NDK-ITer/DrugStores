using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Models.Entities
{
    public class Thuoc
    {
        public Guid MaSP { get; set; }
        public string DonViTinh { get; set; }
        public string ThanhPhan { get; set; }
        public string TacDungPhu { get; set; }
        public string LieuDung { get; set; }
        public int SoLuong { get; set; }
        public virtual SanPham SanPham { get; set; }

    }
}
