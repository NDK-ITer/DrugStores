using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Models.Entities
{
    [Table("TagTinTuc")]
    public class TagTinTuc
    {
        [Key]
        [ForeignKey("Tag")]
        [Column(Order = 0)]
        public string? IdTag { get; set; }
        [Key]
        [ForeignKey("TinTuc")]
        [Column(Order = 1)]
        public Guid? MaTT { get; set; }
        public virtual TinTuc? TinTuc { get; set; }
        public virtual Tag? Tag { get; set; }
    }
}
