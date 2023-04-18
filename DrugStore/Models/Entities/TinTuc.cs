
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DrugStore.Models.Entities
{
    [Table("TinTuc")]
    public partial class TinTuc
    {
        [Key]
        public Guid MaTT { get; set; }

        public string AnhDaiDien { get; set; }

        public DateTime? ThoiGiaDang { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        public string MoTaTT { get; set; }

        public int? SoLuotXem { get; set; }
    }
}
