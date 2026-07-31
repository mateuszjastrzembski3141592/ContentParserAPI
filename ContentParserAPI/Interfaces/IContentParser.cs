using ContentParserAPI.DTOs;

namespace ContentParserAPI.Interfaces;

public interface IContentParser
{
    Task <ParseResponse> ParseData(string data);
}
