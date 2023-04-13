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
    public class HoaDonConfigurations : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.ToTable(nameof(HoaDon));
            builder.HasOne(x => x.AppUser).WithMany(x => x.HoaDons).HasForeignKey(x => x.Id);
            builder.HasKey(x => x.SoDH);
            builder.Property(x => x.TenNguoiMua).IsRequired();
            builder.Property(x => x.SDT).IsRequired();
        }
    }
}