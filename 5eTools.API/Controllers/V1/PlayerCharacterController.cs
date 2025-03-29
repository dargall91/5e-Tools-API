using _5eTools.API.Models;
using _5eTools.Data.Entities;
using _5eTools.Services;
using _5eTools.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace _5eTools.API.Controllers.V1;

[ApiController]
[Route("api/v{version:apiversion}/player-character")]
public class PlayerCharacterController(IPlayerCharacterService pcService, IUserService userService, ICampaignService campaignService) : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        if (pcService.PcExists(id))
        {
            return Ok(new ResponseWrapper<PlayerCharacterDto>(pcService.FindDto(id)));
        }

        return NotFound(new ResponseWrapper<bool>($"No PC with ID {id} found"));
    }

    [HttpPut]
    public IActionResult CreatePlayerCharacter(PlayerCharacterDto pcDto, int campaignId, int userId)
    {
        var errors = new List<string>();
        Campaign? campaign = null;
        User? user = null;

        if (campaignService.CampaignExists(campaignId))
        {
            campaign = campaignService.FindById(campaignId);

            if (campaign.IsDeleted)
            {
                errors.Add("Selected campaign does not exist");
            }
            else if (campaign.IsFinished)
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
            user = userService.FindById(campaignId);

            if (user.Deactivated)
            {
                errors.Add("Selected user does not exist");
            }
        }
        else
        {
            errors.Add("Selected user does not exist");
        }

        if (errors.Count > 0)
        {
            return BadRequest(new ResponseWrapper<bool>(errors));
        }

        var (newPc, newPcId) = pcService.Add(pcDto, campaign!, user!);
        var response = new ResponseWrapper<PlayerCharacterDto>(newPc);

        return CreatedAtAction(nameof(GetById), new { id = newPcId }, response);
    }

    [HttpPost("{id}")]
    public IActionResult UpdatePlayerCharacter(int id, PlayerCharacterDto pcDto)
    {
        if (pcService.PcExists(id))
        {
            return Ok(new ResponseWrapper<PlayerCharacterDto>(pcService.Update(id, pcDto)));
        }

        return NotFound(new ResponseWrapper<bool>($"No PC with ID {id} found"));
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

            return Ok(new ResponseWrapper<bool>());
        }

        return NotFound(new ResponseWrapper<bool>($"No PC with ID {id} found"));
    }

    [HttpPost("{id}/revive")]
    public IActionResult RevivePlayerCharacter(int id)
    {
        if (pcService.PcExists(id))
        {
            pcService.Revive(id);

            return Ok(new ResponseWrapper<bool>());
        }

        return NotFound(new ResponseWrapper<bool>($"No PC with ID {id} found"));
    }
}
