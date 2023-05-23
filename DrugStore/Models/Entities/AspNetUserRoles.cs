using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrugStore.Models.Entities
{
    [Table("AspNetUserRoles")]
    public class AspNetUserRoles
    {
        [Key]
        [ForeignKey("Users")]
        [Column(Order = 0)]
        public string? UserId { get; set; }
        [Key]
        [ForeignKey("Roles")]
        [Column(Order = 1)]
        public string? RoleId { get; set; }
        public virtual AspNetRoles? Roles { get; set; }
        public virtual AspNetUser? Users { get; set; }
    }
}
