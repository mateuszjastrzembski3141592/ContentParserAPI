namespace ContentParserAPI.DTOs;

public class ParseResponse
{
    public required bool Status { get; init; }
    public required int ProcessedCount { get; init; }
    public required IEnumerable<Dictionary<string, object>> Data { get; init; }

    public static ParseResponse DefaultResponse { get; } = new()
    {
        Status = false,
        ProcessedCount = 0,
        Data = []
    };
}
