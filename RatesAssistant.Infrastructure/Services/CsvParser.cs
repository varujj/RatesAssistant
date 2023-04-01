using NReco.Csv;
using System.Text;

namespace RatesAssistant.Infrastructure.Services;

public static class CsvParser
{
    public static List<ParsedRow> GetRows(string text, string delimiter = "|")
    {
        var parsedRows = new List<ParsedRow>();

        using var streamReader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(text)));
        var csvReader = new CsvReader(streamReader, delimiter);

        int rowNum = 0;
        var headerRow = new ParsedRow();

        while (csvReader.Read())
        {
            var row = new ParsedRow();

            if (rowNum == 0)
            {
                row.IsHeader = true;
                headerRow = row;
            }

            for (int i = 0; i < csvReader.FieldsCount; i++)
            {
                if (row.IsHeader && string.IsNullOrEmpty(csvReader[i]))
                    continue;

                if (!row.IsHeader && !headerRow.HasCell(i))
                    continue;

                row.Cells.Add(new ParsedCell
                {
                    ColumnName = row.IsHeader ? csvReader[i] : headerRow[i].ColumnName,
                    Value = csvReader[i].Trim(),
                });
            }

            row.Number = rowNum + 1;
            rowNum++;

            parsedRows.Add(row);
        }

        return parsedRows;
    }
}

public class ParsedRow
{
    public List<ParsedCell> Cells { get; set; }
    public bool IsHeader { get; set; }
    public int Number { get; set; }

    public ParsedRow()
    {
        Cells = new List<ParsedCell>();
    }

    public ParsedCell this[int i]
    {
        get => Cells[i];
    }

    public bool HasCell(int i)
    {
        return Cells.Count > i;
    }
}

public class ParsedCell
{
    public string ColumnName { get; set; }
    public string Value { get; set; }
}