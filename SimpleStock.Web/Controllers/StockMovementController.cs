using Microsoft.AspNetCore.Mvc;
using SimpleStock.Web.Interfaces;
using SimpleStock.Web.Models;

namespace SimpleStock.Web.Controllers;

public class StockMovementController : Controller
{
    private readonly IStockService _stockService;

    public StockMovementController(IStockService stockService)
    {
        _stockService = stockService;
    }

    // GET: StockMovement
    public async Task<IActionResult> Index()
    {
        var movements = await _stockService.GetMovementsAsync();
        return View(movements);
    }

    // GET: StockMovement/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: StockMovement/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Product,Quantity,Type,Supplier,Recipient")] StockMovement stockMovement)
    {
        if (ModelState.IsValid)
        {
            await _stockService.AddMovementAsync(
                stockMovement.Product,
                stockMovement.Quantity,
                stockMovement.Type,
                stockMovement.Supplier,
                stockMovement.Recipient);
            return RedirectToAction(nameof(Index));
        }
        return View(stockMovement);
    }

    // GET: StockMovement/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var stockMovement = await _stockService.GetMovementByIdAsync(id.Value);
        if (stockMovement == null)
        {
            return NotFound();
        }

        return View(stockMovement);
    }

    // POST: StockMovement/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _stockService.DeleteMovementAsync(id);
        return RedirectToAction(nameof(Index));
    }
}