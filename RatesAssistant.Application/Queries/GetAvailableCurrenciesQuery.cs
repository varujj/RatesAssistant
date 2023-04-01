using CSharpFunctionalExtensions;
using RatesAssistant.Infrastructure.Persistence;

namespace RatesAssistant.Application.Queries;

public class GetAvailableCurrenciesQuery
{
    private readonly IExchangeRateRepository _repository;

    public GetAvailableCurrenciesQuery(IExchangeRateRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<string>>> Execute()
    {
        var currencies = await _repository.GetAvailableCurrencies();

        return Result.Success(currencies);
    }
}
