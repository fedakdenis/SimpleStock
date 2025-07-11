using SimpleStock.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace SimpleStock.Web.DTOs;

public class AddMovementRequest
{
    [Required(ErrorMessage = "Product is required")]
    public string Product { get; set; } = string.Empty;

    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Movement type is required")]
    public MovementType Type { get; set; }

    public string? Supplier { get; set; }
    public string? Recipient { get; set; }
}