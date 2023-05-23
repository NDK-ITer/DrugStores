using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Models.Entities
{
    [Table("AspNetRoles")]
    public class AspNetRoles
    {
        public AspNetRoles()
        {
            Users = new HashSet<AspNetUserRoles>();
        }
        [Key]
        public string? Id { get; set; }

        [StringLength(256)]
        public string? Name { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        [StringLength(256)]
        public string? NormalizedName { get; set; }

        public string? ConcurrencyStamp { get; set; }
        public virtual ICollection<AspNetUserRoles>? Users { get; set; }
    }
}
