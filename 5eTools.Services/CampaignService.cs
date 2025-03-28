using _5eTools.Data;
using _5eTools.Data.Entities;
using _5eTools.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace _5eTools.Services;

public interface ICampaignService
{
    bool CampaignExists(int id);
    IEnumerable<Campaign> FindAll();

    /// <summary>
    /// Finds a <see cref="Campaign"/> by it's ID. A Campaign with this ID assumed
    /// to exist. Use  <see cref="CampaignExists"/> to ensure it exists before
    /// calling this method.
    /// </summary>
    /// <param name="id">The ID of the campaign to find</param>
    /// <returns>The <see cref="Campaign"/></returns>
    Campaign FindById(int id);

    Campaign? FindActiveCampaign();
    Campaign AddCampaign(AddEditCampaign campaign);
    void UpdateCampaign(int id, AddEditCampaign campaign);
    void DeleteCampaign(int id);
    void ActivateCampaign(int id);
    List<ClassListItem> FindCampaignClassOptions(int id);
}

public class CampaignService(ToolsDbContext dbContext) : ICampaignService
{
    public bool CampaignExists(int id) => dbContext.Campaigns.Find(id) != default;

    public IEnumerable<Campaign> FindAll() => dbContext.Campaigns.ToList();

    public Campaign FindById(int id) => dbContext.Campaigns.Find(id)!;

    public Campaign? FindActiveCampaign() => dbContext.Campaigns.SingleOrDefault(x => x.IsActive);

    public Campaign AddCampaign(AddEditCampaign newCampaignDetails)
    {
        var newCampaign = new Campaign
        {
            Name = newCampaignDetails.Name,
            UsesInflatedHitPoints = newCampaignDetails.UsesInflatedHitPoints,
            UsesStress = newCampaignDetails.UsesStress,
            IsActive = !dbContext.Campaigns.Any()
        };

        dbContext.Campaigns.Add(newCampaign);
        dbContext.SaveChanges();

        return newCampaign;
    }

    public void UpdateCampaign(int id, AddEditCampaign updatedCampaignDetails)
    {
        var campaignToEdit = FindById(id);
        campaignToEdit.Name = updatedCampaignDetails.Name;
        campaignToEdit.UsesInflatedHitPoints = updatedCampaignDetails.UsesInflatedHitPoints;
        campaignToEdit.UsesStress = updatedCampaignDetails.UsesStress;

        dbContext.SaveChanges();
    }

    public void DeleteCampaign(int id)
    {
        var campaign = FindById(id);
        campaign.IsDeleted = true;

        dbContext.SaveChanges();
    }

    public void ActivateCampaign(int id)
    {
        var campaign = FindById(id)!;

        campaign.IsActive = true;

        var currentActiveCampaign = dbContext.Campaigns.SingleOrDefault(x => x.IsActive);

        if (currentActiveCampaign != default)
        {
            currentActiveCampaign.IsActive = false;
        }

        dbContext.SaveChanges();
    }

    public List<ClassListItem> FindCampaignClassOptions(int id)
    {
        return dbContext.Classes
            .Include(x => x.Subclasses)
            .ThenInclude(s => s.Campaigns.Where(c => c.Id == id))
            .Where(c => c.Subclasses.Any(s => s.Campaigns.Count == 1))
            .Select(c => new ClassListItem
            {
                Id = c.Id,
                Name = c.Name,
                Subclasses = c.Subclasses.Select(s => new ListItem
                {
                    Id = s.Id,
                    Name = s.Name
                })
            })
            .ToList();
    }
}
