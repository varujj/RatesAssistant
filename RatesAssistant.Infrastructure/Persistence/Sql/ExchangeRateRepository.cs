using Microsoft.EntityFrameworkCore;
using NodaTime;
using RatesAssistant.Domain.Entities;
using RatesAssistant.Domain.Extensions;

namespace RatesAssistant.Infrastructure.Persistence.Sql;

public class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly RatesDbContext _dbContext;

    public ExchangeRateRepository(RatesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpsertRates(List<ExchangeRate> rates)
    {
        var (newRates, existingRates) = rates.Partition(r => r.Id == 0);

        foreach (var rate in newRates)
        {
            _dbContext.ExchangeRates.Add(rate);
        }

        foreach (var rate in existingRates)
        {
            _dbContext.ExchangeRates.Update(rate);
        }

        await _dbContext.SaveChangesAsync();
    }

    public Task<List<ExchangeRate>> GetRatesFor(int year)
    {
        return _dbContext
                .ExchangeRates
                .Where(r => r.Date >= new LocalDate(year, 1, 1) &&
                            r.Date <= new LocalDate(year, 12, 31)
                )
                .ToListAsync();
    }

    public async Task<decimal?> TryGetRateFor(string currency, LocalDate date)
    {
        var exchangeRate = await _dbContext
                            .ExchangeRates
                            .Where(r => r.TargetCurrency == currency && 
                                        r.Date <= date)
                            .OrderByDescending(r => r.Date)
                            .Take(1)
                            .FirstOrDefaultAsync();

        return exchangeRate?.Rate;
    }

    public Task<List<string>> GetAvailableCurrencies()
    {
        return _dbContext
                .ExchangeRates
                .Select(r => r.TargetCurrency)
                .Distinct()
                .ToListAsync();
    }
}
