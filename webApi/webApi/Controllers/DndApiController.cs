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
        return Ok(await _apiService.GetAllSpells());
    }

    [HttpGet]
    public IActionResult GetSpell(string index)
    {
        if (index.Length == 0)
            return BadRequest();
        
        var spell = _apiService.GetSpell(index);

        if (spell is not null)
            return Ok(spell);
        else
            return BadRequest(spell);
    }
}
