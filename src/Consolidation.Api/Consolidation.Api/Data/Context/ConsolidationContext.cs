using Microsoft.EntityFrameworkCore;

namespace Consolidation.Api.Data.Context
{
    public class ConsolidationContext(DbContextOptions<ConsolidationContext> options) : DbContext(options)
    {
        public DbSet<Domain.Models.Consolidation> Consolidations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Models.Consolidation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.TotalRevenue). IsRequired().HasPrecision(18, 2);
                entity.Property(e => e.TotalExpense). IsRequired().HasPrecision(18, 2);
                entity.Property(e => e.OpeningBalance). IsRequired().HasPrecision(18, 2);
                entity.Property(e => e.FinalBalance). IsRequired().HasPrecision(18, 2);
                entity.Property(e => e.DateCreateAt). IsRequired().HasPrecision(18, 2);
                entity.Property(e => e.HourCreateAt). IsRequired().HasPrecision(18, 2);
            });
        }
    }
}
