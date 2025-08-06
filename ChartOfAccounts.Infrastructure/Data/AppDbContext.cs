using ChartOfAccounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChartOfAccounts.Infrastructure.Data;

public class AppDbContext : DbContext
{
    private readonly string? _connectionString;

    public AppDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<ChartOfAccount> ChartOfAccounts => Set<ChartOfAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }
}
