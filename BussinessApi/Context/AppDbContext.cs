using BussinessApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BussinessApi.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobTitles> JobTitles { get; set; }
        public DbSet<Employees> Employees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", false, true)
                 .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BussinessDB"));
        }
    }
}
