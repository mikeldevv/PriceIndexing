using PriceIndexing.Api.Services;

namespace PriceIndexing.Api.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BinanceController : ControllerBase
{
    private readonly BinanceService _binanceService;

    public BinanceController(BinanceService binanceService)
    {
        _binanceService = binanceService;
    }

    [HttpPost("price")]
    public async Task<ActionResult<decimal>> GetPrice([FromBody] string symbol)
    {
        try
        {
            var ticker = await _binanceService.GetPrice(symbol);
            return Ok(ticker.Price);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}