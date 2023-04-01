using NodaTime;
using RatesAssistant.Console.Dtos;
using RatesAssistant.Infrastructure.Services;
using System.Globalization;

namespace RatesAssistant.Console;

public static class ExchangeRatesParser
{
    private const string _dateColumn = "Date";

    public static IEnumerable<ExchangeRateDto> Parse(string text)
    {
        var rows = CsvParser.GetRows(text);

        return rows.Where(r => !IsHeaderRow(r))
                   .SelectMany(RatesFromRow);
    }

    private static bool IsHeaderRow(ParsedRow row)
    {
        return row.Cells.Any(c => c.Value == _dateColumn);
    }
    
    private static List<ExchangeRateDto> RatesFromRow(ParsedRow row)
    {
        EnsureHasDate(row);

        var date = RowDate(row);

        return row
                 .Cells
                 .Where(c => c.ColumnName != _dateColumn)
                 .Select(c =>
                 {
                     var column = c.ColumnName.Split(" ");

                     EnsureColumnHasValidValue(column);
                     EnsureTargetAmountIsValid(column, out var targetAmount);

                     return new ExchangeRateDto()
                     {
                         Date = date,
                         Rate = decimal.Parse(c.Value, CultureInfo.InvariantCulture),
                         TargetAmount = targetAmount,
                         TargetCurrency = column[1]
                     };
                 }).ToList();
    }

    private static LocalDate RowDate(ParsedRow row)
        => LocalDate.FromDateTime(
                DateTime.ParseExact(
                            row.Cells
                               .First(c => c.ColumnName == _dateColumn)
                               .Value, "dd.MM.yyyy",
                            null)
           );

    private static void EnsureHasDate(ParsedRow row)
    {
        if (!row.Cells.Any(c => c.ColumnName == _dateColumn))
            throw new InvalidDataException($"No date found in row {row.Number}");
    }

    private static void EnsureColumnHasValidValue(string[] column)
    {
        if (column.Length != 2)
        {
            throw new InvalidDataException(
                "Wrong expected data, not matched column");
        }
    }

    private static void EnsureTargetAmountIsValid(string[] column, out int targetAmount)
    {
        var couldParse = int.TryParse(column[0], out targetAmount);

        if (!couldParse)
        {
            throw new InvalidDataException(
                "Wrong expected data, target amount invalid");
        }
    }
}