using ContentParserAPI.DTOs;
using ContentParserAPI.Interfaces;
using CsvHelper;
using System.Globalization;

namespace ContentParserAPI.Strategies;

public class CsvStrategy : IContentParser
{
    public Task<ParseResponse> ParseData(string data)
    {
        var defaultResponse = new ParseResponse
        {
            Status = false,
            ProcessedCount = 0,
            Data = []
        };

        try
        {
            using StringReader sr = new(data);
            using CsvReader cr = new(sr, CultureInfo.InvariantCulture);

            var results = cr.GetRecords<dynamic>();
            var parsedData = results.Select(d => new Dictionary<string, object>((IDictionary<string, object>)d))
                                    .ToList();

            if (parsedData.Count == 0)
            {
                return Task.FromResult(defaultResponse);
            }

            return Task.FromResult(new ParseResponse
            {
                Status = true,
                ProcessedCount = parsedData.Count,
                Data = parsedData
            });
        }
        catch (Exception)
        {
            return Task.FromResult(defaultResponse);
        }
    }
}
