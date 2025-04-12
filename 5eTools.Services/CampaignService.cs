using _5eTools.Data;
using _5eTools.Data.Entities;
using _5eTools.Services.DTOs;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace _5eTools.Services;

public interface ICampaignService
{
    bool CampaignExists(int id);
    List<CampaignDto> FindAll();

    /// <summary>
    /// Finds a <see cref="Campaign"/> by it's ID. A Campaign with this ID assumed
    /// to exist. Use  <see cref="CampaignExists"/> to ensure it exists before
    /// calling this method.
    /// </summary>
    /// <param name="id">The ID of the campaign to find</param>
    /// <returns>The <see cref="Campaign"/></returns>
    Campaign FindById(int id);

    CampaignDto FindDtoById(int id);

    CampaignDto? FindActiveCampaign();
    Campaign AddCampaign(AddEditCampaign campaign, int userId);
    void UpdateCampaign(int id, AddEditCampaign campaign);
    void DeleteCampaign(int id);
    void ActivateCampaign(int id);
    bool IsCampaignDeleted(int id);
    bool IsCampaignFinished(int id);
    List<ClassDto> ClassList(int id);
    void AddExistingSubclass(int campaignId, int subclassId);
    void AddNewSubclass(int campaignId, NewSubclass newSubclass);
}

public class CampaignService(ToolsDbContext dbContext) : ICampaignService
{
    public bool CampaignExists(int id) => dbContext.Campaigns.Find(id) != default;

    public List<CampaignDto> FindAll()
    {
        return dbContext.Campaigns
            .Include(x => x.Subclasses)
            .ThenInclude(x => x.Class)
            .Select(x => CampaignToDto(x))
            .ToList();
    }

    public Campaign FindById(int id) => dbContext.Campaigns.Find(id)!;

    public CampaignDto FindDtoById(int id)
    {
        var campaign = dbContext.Campaigns
            .Include(x => x.Subclasses)
            .ThenInclude(x => x.Class)
            .Single(x => x.Id == id);

        return CampaignToDto(campaign);
    }

    public CampaignDto? FindActiveCampaign()
    {
        var campaign = dbContext.Campaigns
            .Include(x => x.Subclasses)
            .ThenInclude(x => x.Class)
            .SingleOrDefault(x => x.IsActive);

        return campaign == default ? null : CampaignToDto(campaign);
    }

    public Campaign AddCampaign(AddEditCampaign newCampaignDetails, int userId)
    {
        var newCampaign = new Campaign
        {
            Name = newCampaignDetails.Name,
            UsesInflatedHitPoints = newCampaignDetails.UsesInflatedHitPoints,
            UsesStress = newCampaignDetails.UsesStress,
            IsActive = !dbContext.Campaigns.Any(),
            CampaignOwner = dbContext.Users.Find(userId)!
        };

        dbContext.Campaigns.Add(newCampaign);
        dbContext.SaveChanges();

        return newCampaign;
    }

    public void UpdateCampaign(int id, AddEditCampaign updatedCampaignDetails)
    {
        var campaignToEdit = FindDtoById(id);
        dbContext.Entry(campaignToEdit).CurrentValues.SetValues(updatedCampaignDetails);

        dbContext.SaveChanges();
    }

    public void DeleteCampaign(int id)
    {
        var campaign = dbContext.Campaigns.Find(id)!;
        campaign.IsDeleted = true;

        dbContext.SaveChanges();
    }

    public void ActivateCampaign(int id)
    {
        var campaign = dbContext.Campaigns.Find(id)!;

        campaign.IsActive = true;

        var currentActiveCampaign = dbContext.Campaigns.SingleOrDefault(x => x.IsActive);

        if (currentActiveCampaign != default)
        {
            currentActiveCampaign.IsActive = false;
        }

        dbContext.SaveChanges();
    }

    public bool IsCampaignDeleted(int id)
    {
        return dbContext.Campaigns.Find(id)!.IsDeleted;
    }

    public bool IsCampaignFinished(int id)
    {
        return dbContext.Campaigns.Find(id)!.IsFinished;
    }

    public List<ClassDto> ClassList(int id)
    {
        return dbContext.Campaigns.Include(x => x.Subclasses).ThenInclude(x => x.Class).Single(x => x.Id == id).Subclasses
            .GroupBy(x => x.Class.Id)
            .Select(x => new ClassDto
            {
                Id = x.Key,
                Name = x.First().Class.Name,
                HitDieSize = x.First().Class.HitDieSize,
                ClassAbilityScore = x.First().Class.ClassAbilityScore,
                Subclasses = x.OrderBy(x => x.Name).Select(s => new SubclassDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    ClassName = s.Class.Name,
                    PrimalCompanion = s.PrimalCompanion
                })
            })
            .OrderBy(x => x.Name)
            .ToList();
    }

    public void AddExistingSubclass(int campaignId, int subclassId)
    {
        var campaign = dbContext.Campaigns.Find(campaignId)!;
        var subclass = dbContext.Subclasses.Find(subclassId)!;

        campaign.Subclasses.Add(subclass);

        dbContext.SaveChanges();
    }

    public void AddNewSubclass(int campaignId, NewSubclass newSubclass)
    {
        var campaign = dbContext.Campaigns.Find(campaignId)!;
        var subclass = new Subclass
        {
            Name = newSubclass.Name,
            PrimalCompanion = newSubclass.PrimalCompanion,
            ThirdCaster = newSubclass.ThirdCaster,
            Class = dbContext.Classes.Find(newSubclass.ClassId)!,
            Campaigns = new List<Campaign> { campaign }
        };

        dbContext.Add(subclass);

        dbContext.SaveChanges();
    }

    private static CampaignDto CampaignToDto(Campaign campaign)
    {
        return new CampaignDto
        {
            CampaignId = campaign.Id,
            Name = campaign.Name,
            UsesInflatedHitPoints = campaign.UsesInflatedHitPoints,
            UsesStress = campaign.UsesStress,
            AllowsMulticlassing = campaign.AllowsMulticlassing,
            Classes = campaign.Subclasses
                .GroupBy(x => x.Class.Id)
                .Select(x => new ClassDto
                {
                    Id = x.Key,
                    Name = x.First().Class.Name,
                    HitDieSize = x.First().Class.HitDieSize,
                    ClassAbilityScore = x.First().Class.ClassAbilityScore,
                    Subclasses = x.OrderBy(x => x.Name).Select(s => new SubclassDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        ClassName = s.Class.Name,
                        PrimalCompanion = s.PrimalCompanion
                    })
                })
                .OrderBy(x => x.Name)
        };
    }
}
