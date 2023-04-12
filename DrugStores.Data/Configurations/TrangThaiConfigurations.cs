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
    public class TrangThaiConfigurations : IEntityTypeConfiguration<TrangThai>
    {
        public void Configure(EntityTypeBuilder<TrangThai> builder)
        {
            builder.ToTable(nameof(TrangThai));
            builder.HasKey(x => x.MaTT);
        }
    }
}
