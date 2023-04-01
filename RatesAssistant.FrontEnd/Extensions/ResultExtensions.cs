using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RatesAssistant.FrontEnd.Extensions;
public static class ResultExtensions
{
    public static async Task<IActionResult> ToActionResult<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask;

        var statusCode = result.IsFailure
            ? HttpStatusCode.BadRequest
            : HttpStatusCode.OK;

        object? value = result.IsFailure
            ? result.Error
            : result.Value;

        return new ObjectResult(value)
        {
            StatusCode = (int)statusCode
        };
    }
}
