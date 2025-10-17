using Datlo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Datlo.Configuration.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<ImportedDataModel> ImportedData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImportedDataModel>()
                    .Property(e => e.LastPaymentValue)
                    .HasPrecision(18, 2);


            base.OnModelCreating(modelBuilder);
        }
    }
}