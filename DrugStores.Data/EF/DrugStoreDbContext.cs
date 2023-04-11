using DrugStores.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.EF
{
    public class DrugStoreDbContext : DbContext
    {
        public DrugStoreDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<HoaDon> HoaDons { get; set; }
    }
}
