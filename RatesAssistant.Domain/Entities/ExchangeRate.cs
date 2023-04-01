using NodaTime;

namespace RatesAssistant.Domain.Entities;

public class ExchangeRate
{
    // For EF
    private ExchangeRate() { }

    public long Id { get; set; }
    public LocalDate Date { get; set; }
    public string TargetCurrency { get; set; }

    /// <summary>
    /// Exchange rate of base currency vs. <see cref="TargetCurrency"/>.
    /// </summary>
    public decimal Rate { get; set; }

    public static ExchangeRate Create(LocalDate date, string targetCurrency, decimal rate)
    {
        if (string.IsNullOrEmpty(targetCurrency))
        {
            throw new ArgumentException(
                $"'{nameof(targetCurrency)}' cannot be null or empty", nameof(targetCurrency));
        }

        if (rate <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(rate), "Exchange rate has to be a positive number");
        }

        return new()
        {
            Date = date,
            TargetCurrency = targetCurrency,
            Rate = rate
        };
    }
}
