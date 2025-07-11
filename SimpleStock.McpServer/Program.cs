using SimpleStock.McpServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

var simpleStockApiBaseUrl = builder.Configuration["SimpleStockApiBaseUrl"];
if (simpleStockApiBaseUrl == null)
{
    throw new ArgumentException("SimpleStockApiBaseUrl is not configured");
}

builder.Services.AddHttpClient<ApiStockService>((serviceProvider, client) =>
{
    client.BaseAddress = new Uri(simpleStockApiBaseUrl);
});

var app = builder.Build();

app.MapMcp();

app.Run();