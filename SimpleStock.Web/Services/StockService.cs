using Microsoft.EntityFrameworkCore;
using SimpleStock.Web.Data;
using SimpleStock.Web.Interfaces;
using SimpleStock.Web.Models;

namespace SimpleStock.Web.Services;

public class StockService : IStockService
{
    private readonly StockContext _context;

    public StockService(StockContext context)
    {
        _context = context;
    }

    public async Task<List<StockMovement>> GetMovementsAsync(int? limit = null, DateTime? fromDate = null, DateTime? toDate = null)
    {
        var query = _context.StockMovements.AsQueryable();

        if (fromDate.HasValue)
            query = query.Where(m => m.Date >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(m => m.Date <= toDate.Value);

        query = query.OrderByDescending(m => m.Date);

        if (limit.HasValue)
            query = query.Take(limit.Value);

        return await query.ToListAsync();
    }

    public async Task<List<StockBalance>> GetBalanceAsync()
    {
        var balances = await _context.StockMovements
            .GroupBy(m => m.Product)
            .Select(g => new StockBalance
            {
                Product = g.Key,
                Balance = g.Sum(m => m.Type == MovementType.Income ? m.Quantity : -m.Quantity)
            })
            .Where(b => b.Balance != 0)
            .OrderBy(b => b.Product)
            .ToListAsync();

        return balances;
    }

    public async Task<StockMovement> AddMovementAsync(string product, int quantity, MovementType type, string? supplier = null, string? recipient = null)
    {
        var movement = new StockMovement
        {
            Product = product,
            Quantity = quantity,
            Type = type,
            Supplier = supplier,
            Recipient = recipient,
            Date = DateTime.Now
        };

        _context.StockMovements.Add(movement);
        await _context.SaveChangesAsync();

        return movement;
    }

    public async Task<int> GetProductBalanceAsync(string product)
    {
        var balance = await _context.StockMovements
            .Where(m => m.Product == product)
            .SumAsync(m => m.Type == MovementType.Income ? m.Quantity : -m.Quantity);

        return balance;
    }

    public async Task<StockMovement?> GetMovementByIdAsync(int id)
    {
        return await _context.StockMovements.FindAsync(id);
    }

    public async Task<bool> DeleteMovementAsync(int id)
    {
        var movement = await _context.StockMovements.FindAsync(id);
        if (movement != null)
        {
            _context.StockMovements.Remove(movement);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}