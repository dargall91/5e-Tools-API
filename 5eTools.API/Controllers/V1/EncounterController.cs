using _5eTools.API.Models;
using _5eTools.Services;
using _5eTools.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace _5eTools.API.Controllers.V1;

[ApiController]
[Route("api/v{version:apiversion}/[controller]")]
public class EncounterController(IEncounterService encounterService, ICampaignService campaignService) : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        if (!encounterService.EncounterIdExists(id))
        {
            return NotFound(new ResponseWrapper<object>($"No encounter with ID {id} found"));
        }

        return Ok(new ResponseWrapper<EncounterDto>(encounterService.FindDto(id)));
    }

    [HttpPut]
    public IActionResult CreateEncounter(string name, int campaignId)
    {
        if (!campaignService.CampaignExists(campaignId))
        {
            return BadRequest(new ResponseWrapper<object>($"Invalid campaign ID: {campaignId}"));
        }

        if (encounterService.EncounterNameExists(name, campaignId))
        {
            return BadRequest(new ResponseWrapper<object>($"An encounter with the name {name} already exists on campaign with ID {campaignId}"));
        }

        var (encounterDto, encounterId) = encounterService.Add(name, campaignId);
        var response = new ResponseWrapper<EncounterDto>(encounterDto);

        return CreatedAtAction(nameof(GetById), new { id = encounterId }, response);
    }

    [HttpPost("{id}")]
    public IActionResult UpdateEncounter(int id, EncounterDto encounterDto)
    {
        if (!encounterService.EncounterIdExists(id))
        {
            return NotFound(new ResponseWrapper<object>($"No encounter with ID {id} found"));
        }

        return Ok(new ResponseWrapper<EncounterDto>(encounterService.Update(id, encounterDto)));
    }

    [HttpPost("{id}/archive")]
    public IActionResult ArchiveEncounter(int id)
    {
        if (!encounterService.EncounterIdExists(id))
        {
            return NotFound(new ResponseWrapper<object>($"No encounter with ID {id} found"));
        }

        encounterService.Archive(id);

        return Ok(new ResponseWrapper<bool>(true));
    }

    [HttpPost("{id}/unarchive")]
    public IActionResult UnarchiveEncounter(int id)
    {
        if (!encounterService.EncounterIdExists(id))
        {
            return NotFound(new ResponseWrapper<object>($"No encounter with ID {id} found"));
        }

        encounterService.Unarchive(id);

        return Ok(new ResponseWrapper<bool>(true));
    }
}
