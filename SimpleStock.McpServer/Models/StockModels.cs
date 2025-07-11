namespace SimpleStock.McpServer.Models;

public class StockMovement
{
    public int Id { get; set; }
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public MovementType Type { get; set; }
    public string? Supplier { get; set; }
    public string? Recipient { get; set; }
    public DateTime Date { get; set; }
}

public class StockBalance
{
    public string Product { get; set; } = string.Empty;
    public int Balance { get; set; }
}

public enum MovementType
{
    Income = 1,
    Expense = 2
}

public class MovementResponse
{
    public int Id { get; set; }
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public MovementType Type { get; set; }
    public string? Supplier { get; set; }
    public string? Recipient { get; set; }
    public DateTime Date { get; set; }
}