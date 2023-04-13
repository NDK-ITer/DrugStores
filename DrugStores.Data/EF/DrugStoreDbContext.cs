using DrugStores.Data.Configurations;
using DrugStores.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.EF
{
    public class DrugStoreDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public DrugStoreDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SanPhamConfigurations());
            modelBuilder.ApplyConfiguration(new DanhMucConfigurations());
            modelBuilder.ApplyConfiguration(new CT_HoaDonConfigurations());
            modelBuilder.ApplyConfiguration(new GioHangConfigurations());
            modelBuilder.ApplyConfiguration(new HoaDonConfigurations());
            modelBuilder.ApplyConfiguration(new PhanLoaiSPConfigurations());
            modelBuilder.ApplyConfiguration(new TrangThaiConfigurations());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims").HasKey(x => x.Id);
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);
        }
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "1", NormalizedName = "User" },
                new IdentityRole() { Name = "Memember", ConcurrencyStamp = "1", NormalizedName = "Memember" }
                );
        }

        public DbSet<CT_HoaDon> CT_HoaDons { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<PhanLoaiSP> PhanLoaiSPs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<TrangThai> TrangThaiSPs { get; set; }
    }
}
