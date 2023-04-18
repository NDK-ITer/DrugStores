using DrugStore.Areas.Identity.Data;
using DrugStore.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace DrugStore.Areas.Identity.Data;

public class DrugStoreDbContext : IdentityDbContext<AppNetUser>
{

    public virtual DbSet<CT_HoaDon> CT_HoaDon { get; set; }
    public virtual DbSet<GioHang> GioHangs { get; set; }
    public virtual DbSet<HangSX> HangSXes { get; set; }
    public virtual DbSet<HinhThucThanhToan> HinhThucThanhToans { get; set; }
    public virtual DbSet<HoaDon> HoaDons { get; set; }
    public virtual DbSet<IdentityRole> IdentityRoles { get; set; }
    public virtual DbSet<LoaiSP> LoaiSPs { get; set; }
    public virtual DbSet<SanPham> SanPhams { get; set; }
    public virtual DbSet<Thuoc> Thuocs { get; set; }
    public virtual DbSet<TinTuc> TinTucs { get; set; }
    public virtual DbSet<TrangThai> TrangThais { get; set; }
    public DrugStoreDbContext(DbContextOptions<DrugStoreDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
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
    }

}
