using Microsoft.EntityFrameworkCore;
using SimpleStock.Web.Models;

namespace SimpleStock.Web.Data;

public class StockContext : DbContext
{
    public StockContext(DbContextOptions<StockContext> options) : base(options)
    {
    }

    public DbSet<StockMovement> StockMovements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StockMovement>()
            .Property(e => e.Type)
            .HasConversion<int>();

        base.OnModelCreating(modelBuilder);
    }
}