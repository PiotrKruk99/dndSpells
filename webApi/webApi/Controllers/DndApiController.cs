using Microsoft.AspNetCore.Mvc;
using webApi.Services;
using webApi.Api;

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
    public async Task<IActionResult> GetAllSpellsNames()
    {
        return Ok(await _apiService.GetAllSpells());
    }
}
