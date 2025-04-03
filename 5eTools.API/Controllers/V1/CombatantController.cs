using _5eTools.API.Models;
using _5eTools.Services;
using _5eTools.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace _5eTools.API.Controllers.V1;

[ApiController]
[Route("api/v{version:apiversion}/[controller]")]
public class CombatantController(ICombatantService combatantService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetCombatants()
    {
        return Ok(new ResponseWrapper<List<CombatantDto>>(combatantService.GetCombatants()));
    }

    [HttpPost]
    public IActionResult SetCombatants(IEnumerable<CombatantDto> combatantDtos)
    {
        combatantService.SetCombatantList(combatantDtos);

        return Ok(new ResponseWrapper<bool>(true));
    }
}
