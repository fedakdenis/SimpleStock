using SimpleStock.Web.Models;

namespace SimpleStock.Web.DTOs;

public class MovementResponse
{
    public int Id { get; set; }
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public MovementType Type { get; set; }
    public string TypeDisplay => Type == MovementType.Income ? "Income" : "Expense";
    public string? Supplier { get; set; }
    public string? Recipient { get; set; }
    public DateTime Date { get; set; }

    public static MovementResponse FromMovement(StockMovement movement)
    {
        return new MovementResponse
        {
            Id = movement.Id,
            Product = movement.Product,
            Quantity = movement.Quantity,
            Type = movement.Type,
            Supplier = movement.Supplier,
            Recipient = movement.Recipient,
            Date = movement.Date
        };
    }
}