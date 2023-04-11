using DrugStores.Models.ObjectDB;
using Microsoft.EntityFrameworkCore;

namespace DrugStores.Models
{
    public class DrugStoreDbContext:DbContext
    {
        public DbSet<HoaDon> HoaDons { get; set; }

        private string connectionString;
        public DrugStoreDbContext():base()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json",optional: false);
            var configuration = builder.Build();
            connectionString = configuration.GetConnectionString("DrugStoresDbContextConnection").ToString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
