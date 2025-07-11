using SimpleStock.Web.Models;

namespace SimpleStock.Web.Interfaces;

public interface IStockService
{
    Task<List<StockMovement>> GetMovementsAsync(int? limit = null, DateTime? fromDate = null, DateTime? toDate = null);
    Task<List<StockBalance>> GetBalanceAsync();
    Task<StockMovement> AddMovementAsync(string product, int quantity, MovementType type, string? supplier = null, string? recipient = null);
    Task<int> GetProductBalanceAsync(string product);
    Task<StockMovement?> GetMovementByIdAsync(int id);
    Task<bool> DeleteMovementAsync(int id);
}