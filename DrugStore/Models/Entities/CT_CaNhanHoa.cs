using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Models.Entities
{
    public partial class CT_CaNhanHoa
    {
        public CT_CaNhanHoa() { }
        [Key]
        [ForeignKey("TongHopLoaiSP")]
        public int MaTHLSP { get; set; }
        [Key]
        [ForeignKey("user")]
        public string id { get; set; }
        public int SoLanXem { get; set; }
        public TongHopLoaiSP TongHopLoaiSP { get; set; }
        public AspNetUser user { get; set; }
    }
}
