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
            var deserializedData = JsonSerializer.Deserialize<IEnumerable<Dictionary<string, object>>>(data); // TODO refactor to use stream and async

            if (deserializedData is not null)
            {
                return Task.FromResult(new ParseResponse
                {
                    Status = true,
                    ProcessedCount = deserializedData.Count(),
                    Data = deserializedData
                });
            }

            return Task.FromResult(new ParseResponse
            {
                Status = false,
                ProcessedCount = 0,
                Data = []
            });
        }
        catch (Exception)
        {
            return Task.FromResult(new ParseResponse
            {
                Status = false,
                ProcessedCount = 0,
                Data = []
            });
        }
    }
}
