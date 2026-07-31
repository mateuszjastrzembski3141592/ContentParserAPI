using ContentParserAPI.Enums;
using ContentParserAPI.Interfaces;
using ContentParserAPI.Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddKeyedSingleton<IContentParser, CsvStrategy>(PayloadType.Csv);
builder.Services.AddKeyedSingleton<IContentParser, InternalJsonStrategy>(PayloadType.InternalJson);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();