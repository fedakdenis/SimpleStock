using System.ComponentModel.DataAnnotations;

namespace SimpleStock.Web.Models;

public class StockMovement
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Product is required")]
    [Display(Name = "Product")]
    public string Product { get; set; } = string.Empty;

    [Required(ErrorMessage = "Quantity is required")]
    [Display(Name = "Quantity")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Movement type is required")]
    [Display(Name = "Movement Type")]
    public MovementType Type { get; set; }

    [Display(Name = "Supplier")]
    public string? Supplier { get; set; }

    [Display(Name = "Recipient")]
    public string? Recipient { get; set; }

    [Display(Name = "Date")]
    public DateTime Date { get; set; } = DateTime.Now;
}

public enum MovementType
{
    [Display(Name = "Income")]
    Income = 1,
    [Display(Name = "Expense")]
    Expense = 2
}