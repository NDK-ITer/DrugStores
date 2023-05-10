﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Models.Entities
{
    [Table("TongHopLoaiSP")]
    public class TongHopLoaiSP
    {
        public TongHopLoaiSP()
        {
            CT_CaNhanHoas = new HashSet<CT_CaNhanHoa>();
        }

        [Key]
        [ForeignKey("LoaiThuoc")]
        public Guid MaTHLSP { get; set; }
        public virtual LoaiThuoc? LoaiThuoc { get; set; }
        public virtual ICollection<CT_CaNhanHoa> CT_CaNhanHoas { get; set; }
    }
}