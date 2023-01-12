using Microsoft.AspNetCore.Mvc;
using webApi.Services;

namespace webApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DndApiController : ControllerBase
{
    private readonly ILogger<DndApiController> _logger;
    private IApiService _apiService;

    public DndApiController(ILogger<DndApiController> logger, IApiService apiService)
    {
        _logger = logger;
        _apiService = apiService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSpells()
    {
        var result = await _apiService.GetAllSpells();
        
        if (result is not null)
            return Ok(result);
        else
            return BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetSpell(string index)
    {
        if (index.Length == 0)
            return BadRequest();

        var result = await _apiService.GetSpell(index);

        if (result is not null)
            return Ok(result);
        else
            return BadRequest(result);
    }
}
