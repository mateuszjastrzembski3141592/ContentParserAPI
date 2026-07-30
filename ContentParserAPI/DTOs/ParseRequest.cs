using ContentParserAPI.Enums;

namespace ContentParserAPI.DTOs;

public class ParseRequest
{
    public required PayloadType Type { get; set; }
    public required string Content { get; set; }
}
