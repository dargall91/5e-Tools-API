using _5eTools.API.Models;
using _5eTools.Data.Entities;
using _5eTools.Services;
using _5eTools.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace _5eTools.API.Controllers.V1;

[ApiController]
[Route("api/v{version:apiversion}/[controller]")]
public class EncounterController(IEncounterService encounterService, ICampaignService campaignService) : ControllerBase
{
    [HttpGet("all")]
    public IActionResult GetEncounterList(bool archived)
    {
        var campaign = campaignService.FindActiveCampaign();

        if (campaign == default)
        {
            return BadRequest(new ResponseWrapper<bool>("No active campaign found!"));
        }

        var encounters = encounterService.GetEncounterListItems(archived, campaign.CampaignId);

        return BadRequest(new ResponseWrapper<List<ListItem>>(encounters));
    }

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
    public IActionResult CreateEncounter(string name)
    {
        var campaign = campaignService.FindActiveCampaign();

        if (campaign == default)
        {
            return BadRequest(new ResponseWrapper<MonsterDto>("Cannot create a monster without a campaign"));
        }

        if (encounterService.EncounterNameExists(name, campaign.CampaignId))
        {
            return BadRequest(new ResponseWrapper<object>($"An encounter with the name {name} already exists on in campaign {campaign.Name}"));
        }

        var (encounterDto, encounterId) = encounterService.Add(name, campaign.CampaignId);
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

    [HttpGet("xp-thresholds")]
    public IActionResult GetXpThresholds()
    {
        var thresholds = encounterService.XpThresholds();

        return Ok(new ResponseWrapper<List<EncounterXpThreshold>(thresholds));
    }
}
