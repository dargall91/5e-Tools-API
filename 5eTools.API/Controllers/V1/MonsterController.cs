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
            return Ok(new ResponseWrapper<Monster>(monsterService.FindById(id)));
        }

        return NotFound(new ResponseWrapper<Monster>($"No monster with ID {id} found."));
    }

    [HttpPut]
    public IActionResult AddMonster(string name)
    {
        var campaign = campaignService.FindActiveCampaign();

        if (campaign == null)
        {
            return BadRequest(new ResponseWrapper<Monster>("Cannot create a monster without a campaign"));
        }

        if (monsterService.MonsterExistsForCampaign(campaign.CampaignId, name))
        {
            return BadRequest(new ResponseWrapper<Monster>($"A monster with the name {name} already exists in campaign {campaign.Name}"));
        }

        var monster = monsterService.AddMonster(name, campaign.CampaignId);

        var response = new ResponseWrapper<Monster>(monster);

        return CreatedAtAction(nameof(GetById), new { id = monster.Id }, response);
    }

    [HttpPost("{id}")]
    public IActionResult UpdateMonster(int id, MonsterDto monsterDto)
    {
        if (monsterService.MonsterIdExists(id))
        {
            var monster = monsterService.UpdateMonster(id, monsterDto);

            return Ok(new ResponseWrapper<Monster>(monster));
        }

        return BadRequest(new ResponseWrapper<Monster>($"Invalid Monster ID: {id}"));
    }

    [HttpPut("{id}/copy")]
    public IActionResult CopyMonster(int id, string name)
    {
        if (!monsterService.MonsterIdExists(id))
        {
            return NotFound(new ResponseWrapper<Monster>($"No monster with ID {id} found."));
        }

        var campaign = campaignService.FindActiveCampaign();

        if (campaign == default)
        {
            return BadRequest(new ResponseWrapper<Monster>("Cannot create a monster without a campaign"));
        }

        if (monsterService.MonsterExistsForCampaign(campaign.CampaignId, name))
        {
            return BadRequest(new ResponseWrapper<Monster>($"A monster with the name {name} already exists in campaign {campaign.Name}"));
        }

        var monster = monsterService.CopyMonster(id, name, campaign.CampaignId);

        var response = new ResponseWrapper<Monster>(monster);

        return CreatedAtAction(nameof(GetById), new { id = monster.Id }, response);
    }


    [HttpPost("{id}/archive")]
    public IActionResult ArchiveMonster(int id)
    {
        if (monsterService.MonsterIdExists(id))
        {
            monsterService.SetArchived(id, true);

            return Ok(new ResponseWrapper<bool>(true));
        }

        return NotFound(new ResponseWrapper<Monster>($"No Monster with ID {id} found"));
    }

    [HttpPost("{id}/unarchive")]
    public IActionResult UnarchiveMonster(int id)
    {
        if (monsterService.MonsterIdExists(id))
        {
            monsterService.SetArchived(id, false);

            return Ok(new ResponseWrapper<bool>(true));
        }

        return NotFound(new ResponseWrapper<Monster>($"No Monster with ID {id} found"));
    }

    [HttpGet("all")]
    public IActionResult GetMonsterList(bool archived)
    {
        var monsters = monsterService.GetMonsterListItems(archived);

        return Ok(new ResponseWrapper<List<ListItem>>(monsters));
    }
}
