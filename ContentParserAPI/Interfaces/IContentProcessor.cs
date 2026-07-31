using ContentParserAPI.DTOs;

namespace ContentParserAPI.Interfaces;

public interface IContentProcessor
{
    Task<ParseResponse?> ProcessContentAsync(ParseRequest requestContent);
}
