using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Data.EF
{
    public class DrugStoresDbContextFactory : IDesignTimeDbContextFactory<DrugStoreDbContext>
    {
        public DrugStoreDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DrugStoresDbContextConnection");
            var optionBuilder = new DbContextOptionsBuilder<DrugStoreDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            return  new DrugStoreDbContext(optionBuilder.Options);
        }
    }
}
