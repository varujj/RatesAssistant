using System.Configuration;
using Refit;
using RatesAssistant.Domain.Entities;

namespace RatesAssistant.Console;

public static class CnbExchangeRatesProvider
{
    public static async Task<List<ExchangeRate>> GetRatesFor(int year)
    {
        var ratesText = await PullRatesFromApiAsText(year);

        return ExchangeRatesParser
                .Parse(ratesText)
                .Select(r => r.ToExchangeRate())
                .ToList();
    }

    private static async Task<string> PullRatesFromApiAsText(int year)
    {
        var apiUrl = ConfigurationManager.AppSettings["cnb_api"];
        var client = RestService.For<ICnbClient>(apiUrl);

        var rates = await client.GetRates(year);

        await rates.EnsureSuccessStatusCodeAsync();

        return rates.Content;
    }
}
