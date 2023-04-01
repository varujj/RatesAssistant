using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;

namespace RatesAssistant.Infrastructure.Persistence.Extensions;

public static class ConfigurationExtensions
{
    public static PropertyBuilder<LocalDate> HasDateColumnType(
        this PropertyBuilder<LocalDate> builder) =>
         builder.HasConversion<LocalDateConverter>()
                .HasColumnType("date");
}

public class LocalDateConverter : ValueConverter<LocalDate, DateTime>
{
    public LocalDateConverter() : base(
        d => d.ToDateTimeUnspecified(),
        d => LocalDate.FromDateTime(d))
    {
    }
}
