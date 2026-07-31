using ContentParserAPI.DTOs;
using ContentParserAPI.Interfaces;
using CsvHelper;
using System.Globalization;

namespace ContentParserAPI.Strategies;

public class CsvStrategy : IContentParser
{
    public Task<ParseResponse> ParseData(string data)
    {
        try
        {
            using StringReader sr = new(data);
            using CsvReader cr = new(sr, CultureInfo.InvariantCulture);

            var results = cr.GetRecords<dynamic>();
            var parsedData = results.Select(d => new Dictionary<string, object>((IDictionary<string, object>)d))
                                    .ToList();

            if (parsedData.Count == 0)
            {
                return Task.FromResult(ParseResponse.DefaultResponse);
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
            return Task.FromResult(ParseResponse.DefaultResponse);
        }
    }
}
