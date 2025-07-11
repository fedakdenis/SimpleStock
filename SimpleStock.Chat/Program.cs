using Microsoft.Extensions.AI;
using OllamaSharp;
using ModelContextProtocol.Client;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables();
var configuration = builder.Build();

var ollamaUri = configuration.GetMandatory<string>(Parameters.OllamaUri);
var ollamaModel = configuration.GetMandatory<string>(Parameters.OllamaModel);
var simpleStockMcpUri = configuration.GetMandatory<string>(Parameters.SimpleStockMcpUri);

IChatClient client =
    new ChatClientBuilder(
        new OllamaApiClient(new Uri(ollamaUri), ollamaModel))
    .UseFunctionInvocation()
    .Build();

IMcpClient mcpClient = await McpClientFactory.CreateAsync(
                new SseClientTransport(
                    new SseClientTransportOptions
                    {
                        Endpoint = new Uri(simpleStockMcpUri)
                    }
                ));

IList<McpClientTool> tools = await mcpClient.ListToolsAsync();
foreach (McpClientTool tool in tools)
{
    Console.WriteLine(tool.Name);
}

List<ChatMessage> messages = [];
while (true)
{
    Console.Write("You >> ");
    messages.Add(new(ChatRole.User, Console.ReadLine()));

    Console.Write("SimpleStock >> ");
    List<ChatResponseUpdate> updates = [];
    await foreach (ChatResponseUpdate update in client
        .GetStreamingResponseAsync(messages, new() { Tools = [.. tools] }))
    {
        Console.Write(update);
        updates.Add(update);
    }
    Console.WriteLine();

    messages.AddMessages(updates);
}
