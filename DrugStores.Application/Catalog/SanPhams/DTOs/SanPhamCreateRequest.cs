using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Application.Catalog.SanPhams.DTOs
{
    public class SanPhamCreateRequest
    {
        public Guid MaSP { get; set; }
        public string TenSP { get; set; }
        public string DonViTinh { get; set; }
        public string ThanhPhan { get; set; }
        public string CongDung { get; set; }
        public string LieuDung { get; set; }
        public string TacDungPhu { get; set; }
        public string MoTa { get; set; }
        public string AnhDaiDien { get; set; }
        public Nullable<int> SoLanMua { get; set; }
        public Guid MaLoaiSP { get; set; }
        public Guid MaDM { get; set; }
        public Guid MaTT { get; set; }
        public decimal GiamGia { get; set; }
        public decimal DonGia { get; set; }
    }
}
