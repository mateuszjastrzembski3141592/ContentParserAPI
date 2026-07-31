using ContentParserAPI.DTOs;
using ContentParserAPI.Interfaces;
using System.Text.Json;

namespace ContentParserAPI.Strategies;

public class InternalJsonStrategy : IContentParser
{
    public Task<ParseResponse> ParseData(string data)
    {
        try
        {
            var deserializedData = JsonSerializer.Deserialize<IEnumerable<Dictionary<string, object>>>(data);

            if (deserializedData is null)
            {
                return Task.FromResult(ParseResponse.DefaultResponse);
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
            return Task.FromResult(ParseResponse.DefaultResponse);
        }
    }
}
