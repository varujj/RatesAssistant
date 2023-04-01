using Microsoft.EntityFrameworkCore;
using RatesAssistant.Domain.Entities;
using RatesAssistant.Infrastructure.Persistence.Configuration;

namespace RatesAssistant.Infrastructure.Persistence;

public class RatesDbContext : DbContext
{
    public DbSet<ExchangeRate> ExchangeRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ExchangeRateConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;Database=RatesAssistant;Trusted_Connection=True;"
        );
    }
}
