using ChartOfAccounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChartOfAccounts.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<ChartOfAccount> ChartOfAccounts => Set<ChartOfAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
