using System.ComponentModel.DataAnnotations;

namespace SimpleStock.Web.Models;

public class StockBalance
{
    [Display(Name = "Product")]
    public string Product { get; set; } = string.Empty;

    [Display(Name = "Balance")]
    public int Balance { get; set; }
}