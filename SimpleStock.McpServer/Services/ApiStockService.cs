using System.Text;
using System.Text.Json;
using SimpleStock.McpServer.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace SimpleStock.McpServer.Services;

public class ApiStockService
{
    private readonly HttpClient _httpClient;

    public ApiStockService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "SimpleStock-MCP-Server");
    }

    public async Task<List<StockMovement>> GetMovementsAsync(int? limit = null, DateTime? fromDate = null, DateTime? toDate = null)
    {
        var param = new Dictionary<string, string?>();
        if (limit.HasValue) param["limit"] = $"{limit}";
        if (fromDate.HasValue) param["fromDate"] = $"{fromDate:yyyy-MM-dd}";
        if (toDate.HasValue) param["toDate"] = $"{toDate:yyyy-MM-dd}";

        var requestUri = QueryHelpers.AddQueryString("api/stockmovements", param);
        var response = await _httpClient.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var movements = JsonSerializer.Deserialize<List<MovementResponse>>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return movements?.Select(m => new StockMovement
        {
            Id = m.Id,
            Product = m.Product,
            Quantity = m.Quantity,
            Type = m.Type,
            Supplier = m.Supplier,
            Recipient = m.Recipient,
            Date = m.Date
        }).ToList() ?? new List<StockMovement>();
    }

    public async Task<List<StockBalance>> GetBalanceAsync()
    {
        var response = await _httpClient.GetAsync($"api/stockbalance");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<StockBalance>>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        }) ?? new List<StockBalance>();
    }

    public async Task<StockMovement> AddMovementAsync(string product, int quantity, MovementType type, string? supplier = null, string? recipient = null)
    {
        var request = new
        {
            Product = product,
            Quantity = quantity,
            Type = type,
            Supplier = supplier,
            Recipient = recipient
        };

        var json = JsonSerializer.Serialize(request, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"api/stockmovements", content);

        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        var movementResponse = JsonSerializer.Deserialize<MovementResponse>(responseJson, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return new StockMovement
        {
            Id = movementResponse!.Id,
            Product = movementResponse.Product,
            Quantity = movementResponse.Quantity,
            Type = movementResponse.Type,
            Supplier = movementResponse.Supplier,
            Recipient = movementResponse.Recipient,
            Date = movementResponse.Date
        };
    }

    public async Task<int> GetProductBalanceAsync(string product)
    {
        var response = await _httpClient.GetAsync($"api/stockbalance/{Uri.EscapeDataString(product)}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<int>(json);
    }
}
