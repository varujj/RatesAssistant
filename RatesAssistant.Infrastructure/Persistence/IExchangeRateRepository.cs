using NodaTime;
using RatesAssistant.Domain.Entities;

namespace RatesAssistant.Infrastructure.Persistence;

public interface IExchangeRateRepository
{
    Task<List<string>> GetAvailableCurrencies();
    Task<decimal?> TryGetRateFor(string currency, LocalDate date);
    Task<List<ExchangeRate>> GetRatesFor(int year);
    Task UpsertRates(List<ExchangeRate> rates);
}
