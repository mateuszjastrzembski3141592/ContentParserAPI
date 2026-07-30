using ContentParserAPI.DTOs;
using ContentParserAPI.Interfaces;
using System.Text.Json;

namespace ContentParserAPI.Strategies;

public class InternalJsonStrategy : IContentParser
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
            var deserializedData = JsonSerializer.Deserialize<IEnumerable<Dictionary<string, object>>>(data); // TODO refactor to use stream and async

            if (deserializedData is null)
            {
                return Task.FromResult(defaultResponse);
            }

            return Task.FromResult(new ParseResponse
            {
                Status = true,
                ProcessedCount = deserializedData.Count(),
                Data = deserializedData
            });

        }
        catch (Exception)
        {
            return Task.FromResult(defaultResponse);
        }
    }
}
