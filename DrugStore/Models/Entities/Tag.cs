using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Models.Entities
{
    [Table("Tag")]
    public class Tag
    {
        [Key]
        public string? IdTag { get; set; }
        public virtual ICollection<TagTinTuc>? TagTinTucs { get; set; }
        public List<TinTuc> TinTucs { get; } = new();
    }
}
