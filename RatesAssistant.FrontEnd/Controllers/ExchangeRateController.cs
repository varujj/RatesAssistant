using Microsoft.AspNetCore.Mvc;
using NodaTime;
using RatesAssistant.Application.Queries;
using RatesAssistant.FrontEnd.Extensions;

namespace RatesAssistant.FrontEnd.Controllers;


[ApiController]
[Route("api/exchange-rate")]
public class ExchangeRateController : ControllerBase
{
    [HttpGet("available-currencies")]
    public Task<IActionResult> GetAvailableCurrenciesQuery(
        [FromServices] GetAvailableCurrenciesQuery query)
        => query.Execute()
                .ToActionResult();

    [HttpGet("{currency}/{date}")]
    public Task<IActionResult> GetExchangeRate(
        [FromServices] GetExchangeRateQuery query,
        string currency,
        LocalDate date)
        => query.Execute(currency, date)
                .ToActionResult();
}
