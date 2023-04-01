using RatesAssistant.Console;
using RatesAssistant.Infrastructure.Persistence;
using RatesAssistant.Infrastructure.Persistence.Sql;

var ratesRepository = new ExchangeRateRepository(new RatesDbContext());

var year = 2023;

var ratesFromApi = await CnbExchangeRatesProvider.GetRatesFor(year);
var existingRates = await ratesRepository.GetRatesFor(year);

var newRates = ratesFromApi.Where(rfa => !existingRates.Any(er => er.Date == rfa.Date));

await ratesRepository.UpsertRates(newRates.ToList());