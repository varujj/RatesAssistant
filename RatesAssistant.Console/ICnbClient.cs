using Refit;

namespace RatesAssistant.Console;

public interface ICnbClient
{
    [Get("")]
    Task<ApiResponse<string>> GetRates([Query]int year);
}
