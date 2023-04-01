using NodaTime;
using RatesAssistant.Domain.Entities;

namespace RatesAssistant.Console.Dtos;

public class ExchangeRateDto
{
    public LocalDate Date { get; set; }
    public string TargetCurrency { get; set; }
    public decimal Rate { get; set; }
    public int TargetAmount { get; set; }

    public ExchangeRate ToExchangeRate()
    {
        var rate = Rate / TargetAmount;

        return ExchangeRate.Create(Date, TargetCurrency, rate);
    }
}
