namespace ContentParserAPI.DTOs;

public class ParseResponse
{
    public required bool Status { get; init; }
    public required int ProcessedCount { get; init; }
    public required IEnumerable<Dictionary<string, object>> Data { get; init; }
}
