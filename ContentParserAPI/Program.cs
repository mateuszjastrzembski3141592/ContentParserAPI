using ContentParserAPI.DTOs;
using ContentParserAPI.Enums;
using ContentParserAPI.Interfaces;
using ContentParserAPI.Services;
using ContentParserAPI.Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeyedSingleton<IContentParser, CsvStrategy>(PayloadType.Csv);
builder.Services.AddKeyedSingleton<IContentParser, InternalJsonStrategy>(PayloadType.InternalJson);
builder.Services.AddScoped<IContentProcessor, ContentProcessor>();

var app = builder.Build();

app.MapPost ("/api/v1/parse-content", async (ParseRequest request, IContentProcessor processor) =>
{
    ParseResponse? response = await processor.ProcessContentAsync(request);

    if (response is null)
    {
        return Results.BadRequest("Unsupported content type");
    }

    return Results.Ok(response);
});

app.Run();