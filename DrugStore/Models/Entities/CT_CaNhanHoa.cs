﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Models.Entities
{
    [Table("CT_CaNhanHoa")]
    public partial class CT_CaNhanHoa
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("TongHopLoaiSP")]
        public Guid MaTHLSP { get; set; }
        [Key]
        [Column(Order = 1)]
        //[ForeignKey("user")]
        public string Id { get; set; }
        public int SoLanXem { get; set; }
        public TongHopLoaiSP? TongHopLoaiSP { get; set; }
        //public AspNetUser user { get; set; }
    }
}