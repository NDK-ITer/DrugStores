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
        public DbSet<CT_HoaDon> CT_HoaDons { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<PhanLoaiSP> PhanLoaiSPs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<TrangThai> TrangThaiSPs { get; set; }
        public DbSet<TinTuc> TinTucs { get; set; }
        public DrugStoreDbContext() : base(GetOptions())
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        private static DbContextOptions GetOptions()
        {
            string connectionString = "Server=.;Database=DrugStore;Trusted_Connection=True;TrustServerCertificate=True;";
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}
