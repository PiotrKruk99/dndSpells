using Microsoft.AspNetCore.Mvc;
using webApi.Database;
using webApi.Api;

namespace webApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DndApiController : ControllerBase
{
    private readonly ILogger<DndApiController> _logger;
    private ILiteDBOper _mainbase;

    public DndApiController(ILogger<DndApiController> logger, ILiteDBOper mainbase)
    {
        _logger = logger;
        _mainbase = mainbase;
    }

    [HttpGet]
    public async Task<SpellsList?> GetAllSpellsNames()
    {
        return await DndApi.GetAllSpells();
    }
}