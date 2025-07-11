using System.ComponentModel;
using System.Text;
using ModelContextProtocol.Server;
using SimpleStock.McpServer.Services;
using SimpleStock.McpServer.Models;

namespace SimpleStock.McpServer.Tools;

[McpServerToolType]
public static class StockTools
{
    [McpServerTool]
    [Description("Get list of stock movements with filtering capabilities")]
    public static async Task<string> GetStockMovements(
        ApiStockService stockService,
        [Description("Maximum number of records")] int? limit = null,
        [Description("Start date in YYYY-MM-DD format")] string? fromDate = null,
        [Description("End date in YYYY-MM-DD format")] string? toDate = null)
    {
        DateTime? fromDateTime = null;
        DateTime? toDateTime = null;

        if (!string.IsNullOrEmpty(fromDate) && DateTime.TryParse(fromDate, out var parsedFromDate))
            fromDateTime = parsedFromDate;

        if (!string.IsNullOrEmpty(toDate) && DateTime.TryParse(toDate, out var parsedToDate))
            toDateTime = parsedToDate;

        var movements = await stockService.GetMovementsAsync(limit, fromDateTime, toDateTime);

        if (!movements.Any())
            return "No stock movements found";

        var result = new StringBuilder();
        result.AppendLine("**STOCK MOVEMENTS**");
        result.AppendLine();

        foreach (var movement in movements)
        {
            var typeText = movement.Type == MovementType.Income ? "Income" : "Expense";

            result.AppendLine($"**{movement.Product}** ({movement.Quantity} pcs.)");
            result.AppendLine($"   └ {typeText} • {movement.Date:dd.MM.yyyy HH:mm}");

            if (!string.IsNullOrEmpty(movement.Supplier))
                result.AppendLine($"   └ Supplier: {movement.Supplier}");

            if (!string.IsNullOrEmpty(movement.Recipient))
                result.AppendLine($"   └ Recipient: {movement.Recipient}");

            result.AppendLine();
        }

        return result.ToString();
    }

    [McpServerTool]
    [Description("Get current balance of all products in stock")]
    public static async Task<string> GetStockBalance(ApiStockService stockService)
    {
        var balances = await stockService.GetBalanceAsync();

        if (!balances.Any())
            return "Stock is empty";

        var result = new StringBuilder();
        result.AppendLine("**STOCK BALANCE**");
        result.AppendLine();

        foreach (var balance in balances.OrderBy(b => b.Product))
        {
            result.AppendLine($"**{balance.Product}**: {balance.Balance} pcs.");
        }

        var totalItems = balances.Sum(b => Math.Max(0, b.Balance));
        result.AppendLine();
        result.AppendLine($"**Total items**: {totalItems} pcs.");

        return result.ToString();
    }

    [McpServerTool]
    [Description("Add new stock movement (income or expense)")]
    public static async Task<string> AddStockMovement(
        ApiStockService stockService,
        [Description("Product name")] string product,
        [Description("Product quantity")] int quantity,
        [Description("Movement type: income or expense")] string type,
        [Description("Supplier (for income)")] string? supplier = null,
        [Description("Recipient (for expense)")] string? recipient = null)
    {
        if (string.IsNullOrWhiteSpace(product))
            return "Error: Product name is required";

        if (quantity <= 0)
            return "Error: Quantity must be greater than 0";

        MovementType movementType;
        switch (type.ToLower())
        {
            case "income":
                movementType = MovementType.Income;
                break;
            case "expense":
                movementType = MovementType.Expense;
                break;
            default:
                return "Error: Type must be 'income' or 'expense'";
        }

        var movement = await stockService.AddMovementAsync(product, quantity, movementType, supplier, recipient);

        var typeText = movementType == MovementType.Income ? "Income" : "Expense";

        var result = new StringBuilder();
        result.AppendLine($"**{typeText} added successfully**");
        result.AppendLine();
        result.AppendLine($"**{product}**: {quantity} pcs.");
        result.AppendLine($"Date: {movement.Date:dd.MM.yyyy HH:mm}");

        if (!string.IsNullOrEmpty(supplier))
            result.AppendLine($"Supplier: {supplier}");

        if (!string.IsNullOrEmpty(recipient))
            result.AppendLine($"Recipient: {recipient}");

        return result.ToString();
    }

    [McpServerTool]
    [Description("Get balance of specific product")]
    public static async Task<string> GetProductBalance(
        ApiStockService stockService,
        [Description("Product name")] string product)
    {
        if (string.IsNullOrWhiteSpace(product))
            return "Error: Product name is required";

        var balance = await stockService.GetProductBalanceAsync(product);

        var status = balance > 0 ? "in stock" : balance < 0 ? "deficit" : "out of stock";

        return $"**{product}**: {balance} pcs. ({status})";
    }
}