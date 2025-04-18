using _5eTools.API.Models;
using _5eTools.Data.Entities;
using _5eTools.Services;
using _5eTools.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace _5eTools.API.Controllers.V1;

[Route("api/v{version:apiversion}/[controller]")]
[ApiController]
public class MonsterController(IMonsterService monsterService, ICampaignService campaignService) : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        if (monsterService.MonsterIdExists(id))
        {
            return Ok(new ResponseWrapper<MonsterDto>(monsterService.FindDto(id)));
        }

        return NotFound(new ResponseWrapper<MonsterDto>($"No monster with ID {id} found."));
    }

    [HttpPut]
    public IActionResult AddMonster(string name)
    {
        var campaign = campaignService.FindActiveCampaign();

        if (campaign == null)
        {
            return BadRequest(new ResponseWrapper<MonsterDto>("Cannot create a monster without a campaign"));
        }

        if (monsterService.MonsterExistsForCampaign(campaign.CampaignId, name))
        {
            return BadRequest(new ResponseWrapper<MonsterDto>($"A monster with the name {name} already exists in campaign {campaign.Name}"));
        }

        var monster = monsterService.AddMonster(name, campaign.CampaignId);

        var response = new ResponseWrapper<MonsterDto>(monster);

        return CreatedAtAction(nameof(GetById), new { id = monster.MonsterId }, response);
    }

    [HttpPost]
    public IActionResult UpdateMonster(MonsterDto monsterDto)
    {
        if (monsterService.MonsterIdExists(monsterDto.MonsterId))
        {
            var monster = monsterService.UpdateMonster(monsterDto);

            return Ok(new ResponseWrapper<MonsterDto>(monster));
        }

        return BadRequest(new ResponseWrapper<MonsterDto>($"Invalid Monster ID: {monsterDto.MonsterId}"));
    }

    [HttpPut("{id}/copy")]
    public IActionResult CopyMonster(int id, string name)
    {
        if (!monsterService.MonsterIdExists(id))
        {
            return NotFound(new ResponseWrapper<MonsterDto>($"No monster with ID {id} found."));
        }

        var campaign = campaignService.FindActiveCampaign();

        if (campaign == default)
        {
            return BadRequest(new ResponseWrapper<MonsterDto>("Cannot create a monster without a campaign"));
        }

        if (monsterService.MonsterExistsForCampaign(campaign.CampaignId, name))
        {
            return BadRequest(new ResponseWrapper<MonsterDto>($"A monster with the name {name} already exists in campaign {campaign.Name}"));
        }

        var monster = monsterService.CopyMonster(id, name, campaign.CampaignId);

        var response = new ResponseWrapper<MonsterDto>(monster);

        return CreatedAtAction(nameof(GetById), new { id = monster.MonsterId }, response);
    }


    [HttpPost("{id}/archive")]
    public IActionResult ArchiveMonster(int id)
    {
        if (monsterService.MonsterIdExists(id))
        {
            monsterService.SetArchived(id, true);

            return Ok(new ResponseWrapper<bool>(true));
        }

        return NotFound(new ResponseWrapper<MonsterDto>($"No Monster with ID {id} found"));
    }

    [HttpPost("{id}/unarchive")]
    public IActionResult UnarchiveMonster(int id)
    {
        if (monsterService.MonsterIdExists(id))
        {
            monsterService.SetArchived(id, false);

            return Ok(new ResponseWrapper<bool>(true));
        }

        return NotFound(new ResponseWrapper<MonsterDto>($"No Monster with ID {id} found"));
    }

    [HttpGet("all")]
    public IActionResult GetMonsterList(bool archived)
    {
        var campaign = campaignService.FindActiveCampaign();

        if (campaign == default)
        {
            return BadRequest(new ResponseWrapper<MonsterDto>("No active campaign found!"));
        }

        var monsters = monsterService.GetMonsterListItems(archived, campaign.CampaignId);

        return Ok(new ResponseWrapper<List<ListItem>>(monsters));
    }

    [HttpGet("challenge-ratings")]
    public IActionResult GetChallengeRatings()
    {
        var crs = monsterService.GetChallengeRatings();

        return Ok(new ResponseWrapper<List<ChallengeRating>>(crs));
    }

}
