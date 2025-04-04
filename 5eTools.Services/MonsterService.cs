using _5eTools.Data;
using _5eTools.Data.Entities;
using _5eTools.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace _5eTools.Services;

public interface IMonsterService
{
    bool MonsterIdExists(int id);
    bool AbilityExistsOnMonster(int monsterId, int abilityId);
    bool ActionExistsOnMonster(int monsterId, int actionId);
    bool LegendaryActionExistsOnMonster(int monsterId, int legendaryActionId);
    bool MonsterExistsForCampaign(int campaignId, string name);
    Monster FindById(int id);
    Monster UpdateMonster(int id, MonsterDto monsterDto);
    Monster AddMonster(string name, Campaign campaign);
    Monster CopyMonster(int id, string name, Campaign campaign);
    void SetArchived(int id, bool isArchived);
    List<ListItem> GetMonsterListItems(bool archived);
}

public class MonsterService(ToolsDbContext dbContext) : IMonsterService
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

    public Monster UpdateMonster(int id, MonsterDto monsterDto)
    {
        var toBeUpdated = dbContext.Monsters
            .Include(x => x.Strength)
            .Include(x => x.Dexterity)
            .Include(x => x.Constitution)
            .Include(x => x.Intelligence)
            .Include(x => x.Wisdom)
            .Include(x => x.Charisma)
            .Include(x => x.Abilities)
            .Include(x => x.Actions)
            .Include(x => x.LegendaryActions)
            .Single(x => x.Id == id);

        //update non-navigation properties
        dbContext.Entry(toBeUpdated).CurrentValues.SetValues(monsterDto);

        //update CR
        toBeUpdated.ChallengeRating = dbContext.ChallengeRatings.Find(monsterDto.ChallengeRatingId)!;

        //ability scores
        dbContext.Entry(toBeUpdated.Strength).CurrentValues.SetValues(monsterDto.Strength);
        dbContext.Entry(toBeUpdated.Dexterity).CurrentValues.SetValues(monsterDto.Dexterity);
        dbContext.Entry(toBeUpdated.Constitution).CurrentValues.SetValues(monsterDto.Constitution);
        dbContext.Entry(toBeUpdated.Intelligence).CurrentValues.SetValues(monsterDto.Intelligence);
        dbContext.Entry(toBeUpdated.Wisdom).CurrentValues.SetValues(monsterDto.Wisdom);
        dbContext.Entry(toBeUpdated.Charisma).CurrentValues.SetValues(monsterDto.Charisma);

        //update actions
        toBeUpdated.Abilities = UpdateAbilityList(toBeUpdated.Abilities.ToList(), monsterDto.Abilities);
        toBeUpdated.Actions = UpdateActionList(toBeUpdated.Actions.ToList(), monsterDto.Actions);
        toBeUpdated.LegendaryActions = UpdateLegendaryActionList(toBeUpdated.LegendaryActions.ToList(), monsterDto.LegendaryActions);

        dbContext.SaveChanges();

        return toBeUpdated;
    }

    public Monster AddMonster(string name, Campaign campaign)
    {
        var monster = new Monster
        {
            Name = name,
            Campaign = campaign,
            ChallengeRating = dbContext.ChallengeRatings.OrderBy(x => x.Id).First()
        };

        dbContext.Add(monster);
        dbContext.SaveChanges();

        return monster;
    }

    public Monster CopyMonster(int id, string name, Campaign campaign)
    {
        var newMonster = AddMonster(name, campaign);

        var sourceMonster = dbContext.Monsters
            .Include(x => x.ChallengeRating)
            .Include(x => x.Strength)
            .Include(x => x.Dexterity)
            .Include(x => x.Constitution)
            .Include(x => x.Intelligence)
            .Include(x => x.Wisdom)
            .Include(x => x.Charisma)
            .Include(x => x.Abilities)
            .Include(x => x.Actions)
            .Include(x => x.LegendaryActions)
            .Single(x => x.Id == id);

        CopyEntityExceptId(newMonster, sourceMonster);

        //Name is copied by CopyEntityExceptId, so need to manually set it back it to the new value
        newMonster.Name = name;

        //copy CR
        newMonster.ChallengeRating = dbContext.ChallengeRatings.Find(sourceMonster.ChallengeRating!.Id)!;

        //copy ability scores
        CopyEntityExceptId(newMonster.Strength, sourceMonster.Strength);
        CopyEntityExceptId(newMonster.Dexterity, sourceMonster.Dexterity);
        CopyEntityExceptId(newMonster.Constitution, sourceMonster.Constitution);
        CopyEntityExceptId(newMonster.Intelligence, sourceMonster.Intelligence);
        CopyEntityExceptId(newMonster.Wisdom, sourceMonster.Wisdom);
        CopyEntityExceptId(newMonster.Charisma, sourceMonster.Charisma);

        //copy actions and abilities
        newMonster.Abilities = sourceMonster.Abilities.Select(x => new MonsterAbility
        {
            Name = x.Name,
            Description = x.Description
        }).ToList();

        newMonster.Actions = sourceMonster.Actions.Select(x => new MonsterAction
        {
            Name = x.Name,
            Description = x.Description
        }).ToList();

        newMonster.LegendaryActions = sourceMonster.LegendaryActions.Select(x => new LegendaryAction
        {
            Name = x.Name,
            Description = x.Description,
            Cost = x.Cost
        }).ToList();

        dbContext.SaveChanges();

        return newMonster;
    }

    public void SetArchived(int id, bool isArchived)
    {
        var monster = FindById(id);
        monster.IsArchived = isArchived;

        dbContext.SaveChanges();
    }

    public List<ListItem> GetMonsterListItems(bool archived)
    {
        return dbContext.Monsters
            .Where(x => x.IsArchived == archived)
            .Select(x => new ListItem
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
    }

    private List<MonsterAbility> UpdateAbilityList(List<MonsterAbility> abilityList, IEnumerable<ActionAbilityDto> updatedAbilityList)
    {
        UpdateOrRemoveActionAbility(abilityList, updatedAbilityList);

        //if the new list has more abilities than the old one, add them here
        if (abilityList.Count < updatedAbilityList.Count())
        {
            abilityList.AddRange(updatedAbilityList.Skip(abilityList.Count).Select(x => new MonsterAbility
            {
                Name = x.Name,
                Description = x.Description
            }));
        }

        return abilityList;
    }

    private List<MonsterAction> UpdateActionList(List<MonsterAction> actionList, IEnumerable<ActionAbilityDto> updatedActionList)
    {
        UpdateOrRemoveActionAbility(actionList, updatedActionList);

        //if the new list has more abilities than the old one, add them here
        if (actionList.Count < updatedActionList.Count())
        {
            actionList.AddRange(updatedActionList.Skip(actionList.Count).Select(x => new MonsterAction
            {
                Name = x.Name,
                Description = x.Description
            }));
        }

        return actionList;
    }

    private List<LegendaryAction> UpdateLegendaryActionList(List<LegendaryAction> actionList, IEnumerable<LegendaryActionDto> updatedActionList)
    {
        UpdateOrRemoveActionAbility(actionList, updatedActionList);

        //if the new list has more abilities than the old one, add them here
        if (actionList.Count < updatedActionList.Count())
        {
            actionList.AddRange(updatedActionList.Skip(actionList.Count).Select(x => new LegendaryAction
            {
                Name = x.Name,
                Description = x.Description,
                Cost = x.Cost
            }));
        }

        return actionList;
    }

    private void UpdateOrRemoveActionAbility<TEntity>(List<TEntity> abilityList, IEnumerable<ActionAbilityDto> updatedAbilityList) where TEntity : BaseActionAbility
    {
        //if new list has fewer abilities than the old one, remove abilities until they are the same size
        if (abilityList.Count > updatedAbilityList.Count())
        {
            var toBeRemoved = abilityList.Skip(updatedAbilityList.Count());
            dbContext.RemoveRange(toBeRemoved);
            abilityList.RemoveRange(updatedAbilityList.Count(), abilityList.Count - updatedAbilityList.Count());
        }

        //update list with the incoming values
        abilityList = abilityList.Select((x, i) =>
        {
            dbContext.Entry(x).CurrentValues.SetValues(updatedAbilityList.Skip(i).Take(1).Single());
            return x;
        }).ToList();
    }

    /// <summary>
    /// Copies the values of the <paramref name="source"/> entity into the <paramref name="target"/>
    /// entity, except for the <c>Id</c> property
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="target">The entity to copy values into</param>
    /// <param name="source">The entity to copy</param>
    private void CopyEntityExceptId<TEntity>(TEntity target, TEntity source) where TEntity : class
    {
        var sourceIdProperty = source.GetType().GetProperty("Id")!;
        var targetIdProperty = target.GetType().GetProperty("Id")!;

        //get the original source's ID
        var sourceId = sourceIdProperty.GetValue(source);
        //set the original source's ID as the target's so an exception isn't thrown by SetValues
        sourceIdProperty.SetValue(source, targetIdProperty.GetValue(target)!);

        //copy non-navigation properties.
        dbContext.Entry(target).CurrentValues.SetValues(source);
        //set the source's ID back to it's original value
        sourceIdProperty.SetValue(source, sourceId);
    }
}
