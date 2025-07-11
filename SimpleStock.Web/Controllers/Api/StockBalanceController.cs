using Microsoft.AspNetCore.Mvc;
using SimpleStock.Web.Interfaces;
using SimpleStock.Web.Models;

namespace SimpleStock.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class StockBalanceController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockBalanceController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet]
    public async Task<ActionResult<List<StockBalance>>> GetBalance()
    {
        var balance = await _stockService.GetBalanceAsync();
        return Ok(balance);
    }

    [HttpGet("{product}")]
    public async Task<ActionResult<int>> GetProductBalance(string product)
    {
        var balance = await _stockService.GetProductBalanceAsync(product);
        return Ok(balance);
    }
}