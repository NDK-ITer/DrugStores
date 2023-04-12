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
    public class GioHangConfigurations : IEntityTypeConfiguration<GioHang>
    {
        public void Configure(EntityTypeBuilder<GioHang> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable(nameof(GioHang));
            builder.HasOne(x => x.SanPham).WithMany(x => x.GioHangs).HasForeignKey(x=>x.MaSP);
            builder.Property(x=>x.SoLuong).HasDefaultValue(1);
        }
    }
}
