using ContentParserAPI.DTOs;
using ContentParserAPI.Enums;
using ContentParserAPI.Interfaces;
using System.Text;

namespace ContentParserAPI.Services;

public class ContentProcessor(IServiceProvider service) : IContentProcessor
{
    private readonly IServiceProvider _service = service;

    public async Task<ParseResponse?> ProcessContentAsync(ParseRequest requestContent)
    {
        if (!Enum.IsDefined(requestContent.Type))
        {
            return ParseResponse.DefaultResponse;
        }

        var decodedContent = DecodeContent(requestContent.Content);

        if (decodedContent is null)
        {
            return ParseResponse.DefaultResponse;
        }

        IContentParser? parser = _service.GetKeyedService<IContentParser>(requestContent.Type);

        if (parser is null)
        {
            return ParseResponse.DefaultResponse;
        }

        return await parser.ParseData(decodedContent);
    }

    private static string? DecodeContent(string content)
    {
        if (content is null)
        {
            return null;
        }

        try
        {
            byte[] data = Convert.FromBase64String(content);
            string decodedData = Encoding.UTF8.GetString(data);
        
            return decodedData;

        }
        catch (Exception)
        {
            return null;
        }

    }
}
