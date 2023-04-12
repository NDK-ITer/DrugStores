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
    public class PhanLoaiSPConfigurations : IEntityTypeConfiguration<PhanLoaiSP>
    {
        public void Configure(EntityTypeBuilder<PhanLoaiSP> builder)
        {
            builder.ToTable(nameof(PhanLoaiSP));
            builder.HasKey(x => x.MaLoaiSP);
        }
    }
}