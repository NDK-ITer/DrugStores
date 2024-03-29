﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DrugStore.Models.Entities
{
    [Table("AspNetUsers")]
    public class AspNetUser
    {
        public AspNetUser()
        {
            Roles = new HashSet<AspNetUserRoles>();
            TinTucs = new HashSet<TinTuc>();
        }
        [Key]
        [StringLength(400)]
        public string Id { get; set; }

        [StringLength(200)]
        public string FirstName { get; set; }

        [StringLength(200)]
        public string LastName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreateDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdateDate { get; set; }

        [StringLength(256)]
        [DisplayName("Tên Đăng Nhập")]
        public string UserName { get; set; }

        [StringLength(256)]
        public string NormalizedUserName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(256)]
        public string? NormalizedEmail { get; set; }

        public bool? EmailConfirmed { get; set; }

        public string? PasswordHash { get; set; }

        public string? SecurityStamp { get; set; }

        public string? ConcurrencyStamp { get; set; }

        public string? PhoneNumber { get; set; }

        public bool? PhoneNumberConfirmed { get; set; }

        public bool? TwoFactorEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool? LockoutEnabled { get; set; }

        public int? AccessFailedCount { get; set; }

        public ICollection<HoaDon>? HoaDons { get; set; }
        [DisplayName("Phân Quyền")]
        public virtual ICollection<AspNetUserRoles>? Roles { get; set; }
        public virtual ICollection<TinTuc>? TinTucs { get; set; }
    }
}
