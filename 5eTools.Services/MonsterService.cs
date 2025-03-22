using _5eTools.Data;
using _5eTools.Data.Entities;
using _5eTools.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace _5eTools.Services;

public interface IMonsterService
{
    bool MonsterIdExists(int id);
    bool AbilityExistsOnMonster(int monsterId, int abilityId);
    bool ActionExistsOnMonster(int monsterId, int actionId);
    bool LegendaryActionExistsOnMonster(int monsterId, int legendaryActionId);
    bool MonsterExistsForCampaign(int campaignId, string name);
    Monster FindById(int id);
    void UpdateMonster(Monster monster);
    Monster AddMonster(string name, Campaign campaign);
    Monster CopyMonster(int id, string name, Campaign? targetCampaign);
    void SetArchived(int id, bool isArchived);
    List<MonsterListItem> GetMonsterListItems(bool archived);
    int AddAbility(int id);
    int AddAction(int id);
    int AddLegendaryAction(int id);
    void DeleteAbility(int id);
    void DeleteAction(int id);
    void DeleteLegendaryAction(int id);
}

public class MonsterService(ToolsDbContext dbContext, ICampaignService campaignService) : IMonsterService
{
    public bool MonsterIdExists(int id) => dbContext.Monsters.Find(id) != default;

    public bool AbilityExistsOnMonster(int monsterId, int abilityId)
        => dbContext.Monsters.Any(x => x.Id == monsterId && x.Abilities.Any(x => x.Id == abilityId));

    public bool ActionExistsOnMonster(int monsterId, int actionId)
        => dbContext.Monsters.Any(x => x.Id == monsterId && x.Actions.Any(x => x.Id == actionId));

    public bool LegendaryActionExistsOnMonster(int monsterId, int legendaryActionId)
        => dbContext.Monsters.Any(x => x.Id == monsterId && x.LegendaryActions.Any(x => x.Id == legendaryActionId));

    public bool EntityIdExists<TEntity>(int id) where TEntity : class
    => dbContext.Find<TEntity>(id) != default;

    public bool MonsterExistsForCampaign(int campaignId, string name)
        => dbContext.Monsters.Include(x => x.Campaign).Any(x => x.Name == name && x.Campaign.Id == campaignId);

    public Monster FindById(int id) => dbContext.Monsters.Find(id)!;

    public void UpdateMonster(Monster monster)
    {
        dbContext.Update(monster);
        dbContext.SaveChanges();
    }

    public Monster AddMonster(string name, Campaign campaign)
    {
        var monster = new Monster
        {
            Name = name,
            Campaign = campaignService.FindActiveCampaign()!,
            ChallengeRating = dbContext.ChallengeRatings.OrderBy(x => x.Id).First()
        };

        dbContext.Add(monster);
        dbContext.SaveChanges();

        return monster;
    }

    public Monster CopyMonster(int id, string name, Campaign? targetCampaign)
    {
        //get the monster, set the name name, and reset all IDs to 0 so they can be re-entered as new entities
        //change tracker must be disabled
        dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        var monster = FindById(id);
        monster.Id = 0;
        monster.Name = name;
        monster.Strength.Id = 0;
        monster.Dexterity.Id = 0;
        monster.Constitution.Id = 0;
        monster.Intelligence.Id = 0;
        monster.Wisdom.Id = 0;
        monster.Charisma.Id = 0;

        //if being copied to another campaign
        if (targetCampaign != null)
        {
            monster.Campaign = targetCampaign;
        }

        monster.Abilities = monster.Abilities.Select(x =>
        {
            x.Id = 0;
            return x;
        }).ToList();

        monster.Actions = monster.Actions.Select(x =>
        {
            x.Id = 0;
            return x;
        }).ToList();

        monster.LegendaryActions = monster.LegendaryActions.Select(x =>
        {
            x.Id = 0;
            return x;
        }).ToList();

        dbContext.Add(monster);
        dbContext.SaveChanges();

        dbContext.ChangeTracker.AutoDetectChangesEnabled = true;

        return monster;
    }

    public void SetArchived(int id, bool isArchived)
    {
        var monster = FindById(id);
        monster.IsArchived = isArchived;

        dbContext.SaveChanges();
    }

    public List<MonsterListItem> GetMonsterListItems(bool archived)
    {
        return dbContext.Monsters
            .Where(x => x.IsArchived == archived)
            .Select(x => new MonsterListItem
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
    }

    public int AddAbility(int id)
    {
        var monster = FindById(id);
        var ability = new MonsterAbility();

        monster.Abilities.Add(ability);
        dbContext.SaveChanges();

        return ability.Id;
    }

    public int AddAction(int id)
    {
        var monster = FindById(id);
        var action = new MonsterAction();

        monster.Actions.Add(action);
        dbContext.SaveChanges();

        return action.Id;
    }

    public int AddLegendaryAction(int id)
    {
        var monster = FindById(id);
        var action = new LegendaryAction();

        monster.LegendaryActions.Add(action);
        dbContext.SaveChanges();

        return action.Id;
    }

    public void DeleteAbility(int id)
    {
        var ability = dbContext.MonsterAbilities.Find(id)!;

        dbContext.Remove(ability);
        dbContext.SaveChanges();
    }

    public void DeleteAction(int id)
    {
        var action = dbContext.MonsterActions.Find(id)!;

        dbContext.Remove(action);
        dbContext.SaveChanges();
    }

    public void DeleteLegendaryAction(int id)
    {
        var action = dbContext.LegendaryActions.Find(id)!;

        dbContext.Remove(action);
        dbContext.SaveChanges();
    }
}
