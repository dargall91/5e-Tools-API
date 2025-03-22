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

        return NotFound();
    }

    [HttpPut]
    public IActionResult AddMonster(string name)
    {
        var campaign = campaignService.FindActiveCampaign();

        if (campaign == null)
        {
            return BadRequest(new ResponseWrapper<Monster>("Cannot create a monster without a campaign"));
        }

        if (monsterService.MonsterExistsForCampaign(campaign.Id, name))
        {
            return BadRequest(new ResponseWrapper<Monster>($"A monster with the name {name} already exists in campaign {campaign.Name}"));
        }

        var monster = monsterService.AddMonster(name, campaign);

        var response = new ResponseWrapper<Monster>(monster);

        return CreatedAtAction(nameof(GetById), new { id = monster.Id }, response);
    }

    [HttpPost]
    public IActionResult UpdateMonster(Monster monster)
    {
        if (monsterService.MonsterIdExists(monster.Id))
        {
            monsterService.UpdateMonster(monster);

            return Ok(new ResponseWrapper<Monster>(monster));
        }

        return BadRequest(new ResponseWrapper<Monster>($"Invalid Monster ID: {monster.Id}"));
    }

    [HttpPut("{id}/copy")]
    public IActionResult CopyMonster(int id, string name, int? targetCampaignId)
    {
        if (!monsterService.MonsterIdExists(id))
        {
            return BadRequest(new ResponseWrapper<Monster>($"Invalid Monster ID: {id}"));
        }

        Campaign? campaign = null;

        if (targetCampaignId != null)
        {
            campaign = campaignService.FindActiveCampaign();

            if (campaign == default)
            {
                return BadRequest(new ResponseWrapper<Monster>("Cannot create a monster without a campaign"));
            }
        }

        var monster = monsterService.CopyMonster(id, name, campaign);

        var response = new ResponseWrapper<Monster>(monster);

        return CreatedAtAction(nameof(GetById), new { id = monster.Id }, response);
    }


    [HttpPost("{id}/archive")]
    public IActionResult ArchiveMonster(int id)
    {
        if (monsterService.MonsterIdExists(id))
        {
            monsterService.SetArchived(id, true);
        }

        return NotFound(new ResponseWrapper<Monster>($"No Monster with ID {id} found"));
    }

    [HttpPost("{id}/unarchive")]
    public IActionResult UnarchiveMonster(int id)
    {
        if (monsterService.MonsterIdExists(id))
        {
            monsterService.SetArchived(id, false);
        }

        return NotFound(new ResponseWrapper<Monster>($"No Monster with ID {id} found"));
    }

    [HttpGet("all")]
    public IActionResult GetMonsterList(bool archived)
    {
        var monsters = monsterService.GetMonsterListItems(archived);

        return Ok(new ResponseWrapper<List<MonsterListItem>>(monsters));
    }

    [HttpPut("{id}/ability")]
    public IActionResult AddMonsterAbility(int id)
    {
        if (monsterService.MonsterIdExists(id))
        {
            var abilityId = monsterService.AddAbility(id);

            return Ok(new ResponseWrapper<int>(abilityId));
        }

        return NotFound(new ResponseWrapper<int>($"No Monster with ID {id} found"));
    }

    [HttpPut("{id}/action")]
    public IActionResult AddMonsterAction(int id)
    {
        if (monsterService.MonsterIdExists(id))
        {
            var actionId = monsterService.AddAction(id);

            return Ok(new ResponseWrapper<int>(actionId));
        }

        return NotFound(new ResponseWrapper<int>($"No Monster with ID {id} found"));
    }

    [HttpPut("{id}/legendary-action")]
    public IActionResult AddMonsterLegendaryAction(int id)
    {
        if (monsterService.MonsterIdExists(id))
        {
            var actionId = monsterService.AddLegendaryAction(id);

            return Ok(new ResponseWrapper<int>(actionId));
        }

        return NotFound(new ResponseWrapper<int>($"No Monster with ID {id} found"));
    }

    [HttpDelete("{monsterId}/ability/{abilityId}")]
    public IActionResult DeleteMonsterAbility(int monsterId, int abilityId)
    {
        if (!monsterService.MonsterIdExists(monsterId))
        {
            return NotFound(new ResponseWrapper<Monster>($"No Monster with ID {monsterId} found"));
        }

        if (!monsterService.AbilityExistsOnMonster(monsterId, abilityId))
        {
            return NotFound(new ResponseWrapper<Monster>($"No Ability with ID {abilityId} found on Monster with ID {monsterId}"));
        }

        return NoContent();
    }

    [HttpDelete("{monsterId}/action/{abilityId}")]
    public IActionResult DeleteAction(int monsterId, int actionId)
    {
        if (!monsterService.MonsterIdExists(monsterId))
        {
            return NotFound(new ResponseWrapper<Monster>($"No Monster with ID {monsterId} found"));
        }

        if (!monsterService.ActionExistsOnMonster(monsterId, actionId))
        {
            return NotFound(new ResponseWrapper<Monster>($"No Ability with ID {actionId} found on Monster with ID {monsterId}"));
        }

        return NoContent();
    }
    [HttpDelete("{monsterId}/legendary-action/{abilityId}")]
    public IActionResult DeleteLegendaryAction(int monsterId, int legendaryActionId)
    {
        if (!monsterService.MonsterIdExists(monsterId))
        {
            return NotFound(new ResponseWrapper<Monster>($"No Monster with ID {monsterId} found"));
        }

        if (!monsterService.LegendaryActionExistsOnMonster(monsterId, legendaryActionId))
        {
            return NotFound(new ResponseWrapper<Monster>($"No Ability with ID {legendaryActionId} found on Monster with ID {monsterId}"));
        }

        return NoContent();
    }
}
