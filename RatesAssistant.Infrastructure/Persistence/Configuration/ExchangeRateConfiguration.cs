using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RatesAssistant.Domain.Entities;
using RatesAssistant.Infrastructure.Persistence.Extensions;

namespace RatesAssistant.Infrastructure.Persistence.Configuration;

public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.ToTable("ExchangeRates");

        builder.Property(r => r.Date).HasDateColumnType();
        builder.Property(r => r.TargetCurrency).HasColumnType("char(3)");
        builder.Property(r => r.Rate).HasColumnType("decimal(12,6)");

        builder.HasIndex(r => new { r.Date, r.TargetCurrency }).IsUnique();
    }
}

