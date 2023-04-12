using DrugStores.Data.Configurations;
using DrugStores.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.EF
{
    public class DrugStoreDbContext : IdentityDbContext
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
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<CT_HoaDon> CT_HoaDons { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<PhanLoaiSP> PhanLoaiSPs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<TrangThai> TrangThaiSPs { get;set; }
    }
}
