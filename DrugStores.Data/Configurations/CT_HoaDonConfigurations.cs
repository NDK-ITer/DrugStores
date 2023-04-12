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
    public class CT_HoaDonConfigurations : IEntityTypeConfiguration<CT_HoaDon>
    {
        public void Configure(EntityTypeBuilder<CT_HoaDon> builder)
        {
            builder.HasKey(x => new { x.SoDH, x.MaSP });
            builder.ToTable(nameof(CT_HoaDon));
            builder.Property(x => x.SoLuong).HasDefaultValue(0);
            builder.HasOne(x=>x.HoaDon).WithMany(x=>x.CTHoaDon).HasForeignKey(x=>x.SoDH);
            builder.HasOne(x => x.SanPham).WithMany(x => x.CTHoaDon).HasForeignKey(x => x.MaSP);
        }
    }
}
