using Microsoft.AspNetCore.Mvc;
using SimpleStock.Web.DTOs;
using SimpleStock.Web.Interfaces;

namespace SimpleStock.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class StockMovementsController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockMovementsController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MovementResponse>>> GetMovements(
        [FromQuery] int? limit = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null)
    {
        var movements = await _stockService.GetMovementsAsync(limit, fromDate, toDate);
        var response = movements.Select(MovementResponse.FromMovement).ToList();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovementResponse>> GetMovement(int id)
    {
        var movement = await _stockService.GetMovementByIdAsync(id);
        if (movement == null)
            return NotFound();

        return Ok(MovementResponse.FromMovement(movement));
    }

    [HttpPost]
    public async Task<ActionResult<MovementResponse>> AddMovement([FromBody] AddMovementRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var movement = await _stockService.AddMovementAsync(
            request.Product,
            request.Quantity,
            request.Type,
            request.Supplier,
            request.Recipient);

        var response = MovementResponse.FromMovement(movement);
        return CreatedAtAction(nameof(GetMovement), new { id = movement.Id }, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovement(int id)
    {
        var deleted = await _stockService.DeleteMovementAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}