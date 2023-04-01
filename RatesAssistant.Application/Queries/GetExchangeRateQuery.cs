using CSharpFunctionalExtensions;
using NodaTime;
using RatesAssistant.Infrastructure.Persistence;

namespace RatesAssistant.Application.Queries;
public class GetExchangeRateQuery
{
    private readonly IExchangeRateRepository _repository;

    public GetExchangeRateQuery(IExchangeRateRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<decimal>> Execute(string currency, LocalDate date)
    {
        var rate = await _repository.TryGetRateFor(currency, date);

        if (rate is null)
            return Result.Failure<decimal>("Rate not found");

        return Result.Success(rate.Value);
    }
}