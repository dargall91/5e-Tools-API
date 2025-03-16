using _5eTools.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace _5eTools.API.Controllers.V1;

/// <remarks>
/// Endpoints to view, create, and modify campaigns
/// </remarks>
[Route("api/[controller]")]
[ApiController]
public class CampaignController : ControllerBase
{
    /// <remarks>
    /// Gets a list of all campaigns
    /// </remarks>
    /// <returns>The list of all campaigns by name and their Ids</returns>
    [HttpGet("all")]
    public IActionResult CampaignList()
    {
        return Ok();
    }

    /// <remarks>
    /// Gets a specific campaign's details by it's Id
    /// </remarks>
    /// <returns>The campaign details</returns>
    [HttpGet("{id}")]
    public IActionResult CampaignList(int id)
    {
        return Ok();
    }

    /// <remarks>
    /// Creates a new Campaign
    /// </remarks>
    [HttpPut]
    public IActionResult AddCampaign(Campaign campaign)
    {
        return Ok();
    }

    /// <remarks>
    /// Edit an existing Campaign
    /// </remarks>
    [HttpPost]
    public IActionResult EditCampaign(Campaign campaign)
    {
        return Ok();
    }

    /// <remarks>
    /// Gets the currently active campaign
    /// </remarks>
    [HttpGet("active")]
    public IActionResult GetActive(int id)
    {
        return Ok();
    }

    /// <remarks>
    /// Sets the selected campaign as Active and deactivates the currently active Campaign
    /// </remarks>
    [HttpPost("{id}/activate")]
    public IActionResult SetActive(int id)
    {
        return Ok();
    }

    // <remarks>
    /// Soft deletes the selected campaign
    /// </remarks>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}
