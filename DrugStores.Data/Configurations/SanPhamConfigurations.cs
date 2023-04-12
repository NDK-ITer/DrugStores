using DrugStores.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.Configurations
{
    public class SanPhamConfigurations : IEntityTypeConfiguration<SanPham>
    {
        public void Configure(EntityTypeBuilder<SanPham> builder)
        {
            builder.ToTable(nameof(SanPham));
            builder.HasKey(x => x.MaSP);
            builder.Property(x => x.TenSP).IsRequired();
            builder.HasOne(x => x.PhanLoaiSP).WithMany(x => x.SanPhams).HasForeignKey(x => x.MaLoaiSP);
            builder.HasOne(x => x.DanhMuc).WithMany(x => x.SanPhams).HasForeignKey(x => x.MaDM);
            builder.HasOne(x => x.TrangThai).WithMany(x => x.SanPhams).HasForeignKey(x => x.MaTT);
            builder.Property(x=>x.DonGia) .IsRequired().HasDefaultValue(0);
            builder.Property(x=>x.SoLanMua).HasDefaultValue(0);

        }
    }
}
