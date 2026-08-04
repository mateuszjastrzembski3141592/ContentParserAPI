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
        var type = ParsePayloadType(requestContent.Type);

        if (type is null)
        {
            return null;
        }

        var decodedContent = DecodeContent(requestContent.Content);

        if (decodedContent is null)
        {
            return ParseResponse.DefaultResponse;
        }

        IContentParser? parser = _service.GetKeyedService<IContentParser>(type);

        if (parser is null)
        {
            return ParseResponse.DefaultResponse;
        }

        return await parser.ParseData(decodedContent);
    }

    private static PayloadType? ParsePayloadType(string payloadType) => payloadType.ToUpperInvariant() switch
    {
        "CSV" => PayloadType.Csv,
        "INTERNAL_JSON" => PayloadType.InternalJson,
        _ => null
    };

    private static string? DecodeContent(string content)
    {
        if (content is null)
        {
            return null;
        }

        byte[] data = new byte[content.Length / 4 * 3];

        if (Convert.TryFromBase64String(content, data, out int bytesWritten))
        {
            string decodedData = Encoding.UTF8.GetString(data, 0, bytesWritten);
            return decodedData;
        }

        return null;
    }
}
