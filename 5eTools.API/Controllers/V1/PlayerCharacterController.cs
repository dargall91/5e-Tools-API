using _5eTools.API.Models;
using _5eTools.Services;
using _5eTools.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace _5eTools.API.Controllers.V1;

[ApiController]
[Route("api/v{version:apiversion}/player-character")]
public class PlayerCharacterController(IPlayerCharacterService pcService, IUserService userService, ICampaignService campaignService) : ControllerBase
{
    [HttpGet("master-data")]
    public IActionResult MasterData(int campaignId)
    {
        if (campaignService.CampaignExists(campaignId))
        {
            return Ok(new ResponseWrapper<PlayerCharacterMasterData>(pcService.MasterData(campaignId)));
        }

        return BadRequest(new ResponseWrapper<bool>($"Invalid Campaign ID: {campaignId}"));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        if (pcService.PcExists(id))
        {
            return Ok(new ResponseWrapper<PlayerCharacterDto>(pcService.FindDto(id)));
        }

        return NotFound(new ResponseWrapper<bool>($"No PC with ID {id} found"));
    }

    [HttpGet]
    public IActionResult GetByCampaignAndUser(int campaignId, int userId, bool isDead)
    {
        var errors = ValidateCampaignAndUser(campaignId, userId);

        if (errors.Count > 0)
        {
            return BadRequest(new ResponseWrapper<bool>(errors));
        }

        return Ok(new ResponseWrapper<List<PlayerCharacterDto>>(pcService.GetByCampaignAndUser(campaignId, userId, isDead)));
    }

    [HttpPut]
    public IActionResult CreatePlayerCharacter(PlayerCharacterDto pcDto, int campaignId, int userId)
    {
        var errors = ValidateCampaignAndUser(campaignId, userId);

        if (errors.Count > 0)
        {
            return BadRequest(new ResponseWrapper<bool>(errors));
        }

        var campaign = campaignService.FindById(campaignId);
        var user = userService.FindById(userId);

        var newPc = pcService.Add(pcDto, campaign, user);
        var response = new ResponseWrapper<PlayerCharacterDto>(newPc);

        return CreatedAtAction(nameof(GetById), new { id = newPc.PlayerCharacterId }, response);
    }

    [HttpPost]
    public IActionResult UpdatePlayerCharacter(PlayerCharacterDto pcDto)
    {
        if (pcService.PcExists(pcDto.PlayerCharacterId))
        {
            return Ok(new ResponseWrapper<PlayerCharacterDto>(pcService.Update(pcDto)));
        }

        return NotFound(new ResponseWrapper<bool>($"No PC with ID {pcDto.PlayerCharacterId} found"));
    }

    [HttpPost("base")]
    public IActionResult UpdatePlayerCharacterBase(PlayerCharacterDto pcDto)
    {
        if (pcService.PcExists(pcDto.PlayerCharacterId))
        {
            return Ok(new ResponseWrapper<PlayerCharacterDto>(pcService.UpdateBase(pcDto)));
        }

        return NotFound(new ResponseWrapper<bool>($"No PC with ID {pcDto.PlayerCharacterId} found"));
    }

    [HttpPost("{id}/stress")]
    public IActionResult UpdatePlayerCharacterStress(int id, StressDto stresDto)
    {
        if (pcService.PcExists(id))
        {
            return Ok(new ResponseWrapper<StressDto>(pcService.UpdateStress(id, stresDto)));
        }

        return NotFound(new ResponseWrapper<bool>($"No PC with ID {id} found"));
    }

    [HttpPost("{id}/rest")]
    public IActionResult LongRest(int id, int extendedRestDuration)
    {
        if (pcService.PcExists(id))
        {
            return Ok(new ResponseWrapper<PlayerCharacterDto>(pcService.LongRest(id, extendedRestDuration)));
        }

        return NotFound(new ResponseWrapper<bool>($"No PC with ID {id} found"));
    }

    [HttpPost("{id}/kill")]
    public IActionResult KillPlayerCharacter(int id)
    {
        if (pcService.PcExists(id))
        {
            pcService.Kill(id);

            return Ok(new ResponseWrapper<bool>(true));
        }

        return NotFound(new ResponseWrapper<bool>($"No PC with ID {id} found"));
    }

    [HttpPost("{id}/revive")]
    public IActionResult RevivePlayerCharacter(int id)
    {
        if (pcService.PcExists(id))
        {
            pcService.Revive(id);

            return Ok(new ResponseWrapper<bool>(true));
        }

        return NotFound(new ResponseWrapper<bool>($"No PC with ID {id} found"));
    }

    [HttpPost("/combatant")]
    public IActionResult UpdateCombatantData(PlayerCharacterCombatantDto pcDto)
    {
        if (pcService.PcExists(pcDto.PlayerCharacterId))
        {
            var updatedDto = pcService.UpdateCombatantData(pcDto);

            return Ok(new ResponseWrapper<PlayerCharacterCombatantDto>(updatedDto));
        }

        return BadRequest(new ResponseWrapper<PlayerCharacterCombatantDto>($"No PC with ID {pcDto.PlayerCharacterId} found"));
    }

    private List<string> ValidateCampaignAndUser(int campaignId, int userId)
    {
        var errors = new List<string>();

        if (campaignService.CampaignExists(campaignId))
        {
            if (campaignService.IsCampaignDeleted(campaignId))
            {
                errors.Add("Selected campaign does not exist");
            }
            else if (campaignService.IsCampaignFinished(campaignId))
            {
                errors.Add("Selected is complete - new characters cannot be added");
            }
            else
            {
                // valid campaign
            }
        }
        else
        {
            errors.Add("Selected campaign does not exist");
        }

        if (userService.UserExists(userId))
        {
            var user = userService.FindById(userId);

            if (user.Deactivated)
            {
                errors.Add("Selected user does not exist");
            }
        }
        else
        {
            errors.Add("Selected user does not exist");
        }

        return errors;
    }
}
