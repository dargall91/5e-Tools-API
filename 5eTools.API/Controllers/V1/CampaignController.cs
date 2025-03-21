using _5eTools.API.Models;
using _5eTools.Data.Entities;
using _5eTools.Services;
using _5eTools.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace _5eTools.API.Controllers.V1;

/// <remarks>
/// Endpoints to view, create, and modify campaigns
/// </remarks>
[Route("api/v{version:apiversion}/[controller]")]
[ApiController]
public class CampaignController(ICampaignService campaignService) : ControllerBase
{
    /// <remarks>
    /// Gets a list of all campaigns
    /// </remarks>
    /// <returns>The list of all campaigns by name and their Ids</returns>
    [HttpGet("all")]
    public IActionResult CampaignList()
    {
        var campaigns = campaignService.FindAll();

        var response = new ResponseWrapper<IEnumerable<Campaign>>(campaigns);

        return Ok(response);
    }

    /// <remarks>
    /// Gets a specific campaign's details by it's Id
    /// </remarks>
    /// <returns>The campaign details</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        if (campaignService.CampaignExists(id))
        {
            return NotFound(new ResponseWrapper<object>($"No campaign if ID {id} found"));
        }

        var response = new ResponseWrapper<Campaign>(campaignService.FindById(id));

        return Ok(response);
    }

    /// <remarks>
    /// Creates a new Campaign
    /// </remarks>
    [HttpPut]
    public IActionResult AddCampaign(AddEditCampaign campaign)
    {
        var newCampaign = campaignService.AddCampaign(campaign);

        var response = new ResponseWrapper<Campaign>(newCampaign);

        return CreatedAtAction(nameof(GetById), new { id = newCampaign.Id }, response);
    }

    /// <remarks>
    /// Edits an existing Campaign
    /// </remarks>
    [HttpPost("{id}")]
    public IActionResult EditCampaign(int id, AddEditCampaign campaign)
    {
        if (campaignService.CampaignExists(id))
        {
            return NotFound(new ResponseWrapper<object>($"No campaign if ID {id} found"));
        }

        campaignService.UpdateCampaign(id, campaign);

        return Ok(new ResponseWrapper<object>());
    }

    /// <remarks>
    /// Gets the currently active campaign, or null if there are no active campaigns
    /// </remarks>
    [HttpGet("active")]
    [ProducesResponseType(typeof(ResponseWrapper<Campaign>), StatusCodes.Status200OK)]
    public IActionResult GetActive()
    {
        var campaign = campaignService.FindActiveCampaign();

        var response = new ResponseWrapper<Campaign>(campaign);

        return Ok(response);
    }

    /// <remarks>
    /// Sets the selected campaign as Active and deactivates the currently active Campaign
    /// </remarks>
    [HttpPost("{id}/activate")]
    public IActionResult SetActive(int id)
    {
        if (campaignService.CampaignExists(id))
        {
            return NotFound(new ResponseWrapper<object>($"No campaign if ID {id} found"));
        }

        if (campaignService.FindActiveCampaign()?.Id == id)
        {
            return Ok(new ResponseWrapper<object>("Campaign is already active", "Info"));
        }

        campaignService.ActivateCampaign(id);

        return Ok();
    }

    /// <remarks>
    /// Soft deletes the selected campaign
    /// </remarks>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (campaignService.CampaignExists(id))
        {
            return NotFound(new ResponseWrapper<object>($"No campaign if ID {id} found"));
        }

        if (campaignService.FindActiveCampaign()?.Id == id)
        {
            return BadRequest(new ResponseWrapper<object>("The active campaign cannot be deleted"));
        }

        campaignService.DeleteCampaign(id);

        return NoContent();
    }
}
