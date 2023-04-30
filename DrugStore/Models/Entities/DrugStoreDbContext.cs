using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace DrugStore.Models.Entities
{
    public class DrugStoreDbContext : DbContext
    {
        public virtual DbSet<CT_HoaDon> CT_HoaDon { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<HangSX> HangSXes { get; set; }
        public virtual DbSet<HinhThucThanhToan> HinhThucThanhToans { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<LoaiSP> LoaiSPs { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Thuoc> Thuocs { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }
        public virtual DbSet<TrangThai> TrangThais { get; set; }
        public virtual DbSet<LoaiThuoc> LoaiThuocs { get; set; }

        private string connectionStrings;
        public DrugStoreDbContext() : base()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            connectionStrings = configuration.GetConnectionString("DrugStoreDbContextConnection").ToString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionStrings);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CT_HoaDon>()
                    .HasKey(m => new { m.SoDH, m.MaSP });
            builder.Entity<GioHang>()
                    .HasKey(m => new { m.Id, m.MaSP });
            builder.Entity<SanPham>()
                    .HasOne(e => e.Thuoc)
                    .WithOne(e => e.SanPham)
                    .HasForeignKey<Thuoc>(e => e.MaSP)
                    .IsRequired();
            builder.Entity<Thuoc>()
                    .HasOne(e => e.SanPham)
                    .WithOne(e => e.Thuoc)
                    .HasForeignKey<Thuoc>(e => e.MaSP)
                    .IsRequired();
            builder.Entity<TrangThai>()
                    .HasMany(e => e.SanPhams)
                    .WithOne(e => e.TrangThai)
                    .HasForeignKey(e => e.MaTT)
                    .IsRequired();
            builder.Entity<LoaiSP>()
                    .HasMany(e => e.SanPhams)
                    .WithOne(e => e.LoaiSP)
                    .HasForeignKey(e => e.MaLoaiSP)
                    .IsRequired();
            builder.Entity<LoaiThuoc>()
                    .HasMany(e => e.Thuocs)
                    .WithOne(e => e.LoaiThuoc)
                    .HasForeignKey(e => e.MaLT)
                    .IsRequired();
            builder.Entity<HinhThucThanhToan>()
                    .HasMany(e => e.HoaDons)
                    .WithOne(e => e.HinhThucThanhToan)
                    .HasForeignKey(e => e.MaHT)
                    .IsRequired();
        }
    }
}
