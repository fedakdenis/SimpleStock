using Microsoft.AspNetCore.Mvc;
using SimpleStock.Web.Interfaces;

namespace SimpleStock.Web.Controllers;

public class StockBalanceController : Controller
{
    private readonly IStockService _stockService;

    public StockBalanceController(IStockService stockService)
    {
        _stockService = stockService;
    }

    // GET: StockBalance
    public async Task<IActionResult> Index()
    {
        var balances = await _stockService.GetBalanceAsync();
        return View(balances);
    }
}