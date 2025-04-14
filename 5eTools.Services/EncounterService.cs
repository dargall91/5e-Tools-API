using _5eTools.Data;
using _5eTools.Data.Entities;
using _5eTools.Services.DTOs;
using Microsoft.EntityFrameworkCore;
namespace _5eTools.Services;

public interface IEncounterService
{
    List<ListItem> GetEncounterListItems(bool archived, int campaignId);
    bool EncounterIdExists(int id);
    bool EncounterNameExists(string name, int campaignId);
    EncounterDto FindDto(int id);
    EncounterDto Add(string name, int campaignId);
    EncounterDto Update(EncounterDto encounterDto);
    void Archive(int id);
    void Unarchive(int id);
    List<EncounterXpThreshold> XpThresholds();
}

public class EncounterService(ToolsDbContext dbContext) : IEncounterService
{
    public List<ListItem> GetEncounterListItems(bool archived, int campaignId)
    {
        return dbContext.Encounters
            .Include(x => x.Campaign)
            .Where(x => x.Campaign.Id == campaignId && x.IsArchived == archived)
            .Select(x => new ListItem
            {
                Id = x.Id,
                Name = x.Name
            })
            .OrderBy(x => x.Name)
            .ToList();
    }

    public bool EncounterIdExists(int id) => dbContext.Encounters.Find(id) != default;

    public bool EncounterNameExists(string name, int campaignId)
        => dbContext.Encounters.Include(x => x.Campaign).Any(x => x.Campaign.Id == campaignId && x.Name == name);

    public EncounterDto FindDto(int id)
    {
        return dbContext.Encounters
            .Include(x => x.Music)
            .Include(x => x.EncounterMonsters)
                .ThenInclude(x => x.Monster)
                    .ThenInclude(x => x.Dexterity)
            .Include(x => x.EncounterMonsters)
                .ThenInclude(x => x.Monster)
                    .ThenInclude(x => x.ChallengeRating)
            .Select(e => new EncounterDto
            {
                EncounterId = e.Id,
                Name = e.Name,
                HasLairAction = e.HasLairAction,
                MusicId = e.Music.Id,
                EncounterMonsters = e.EncounterMonsters.Select(em => new EncounterMonsterDto
                {
                    MonsterId = em.Monster.Id,
                    Name = em.Monster.Name,
                    Quantity = em.Quantity,
                    InitiativeRoll = em.InitiativeRoll,
                    IsInvisible = em.IsInvisible,
                    IsReinforcement = em.IsReinforcement,
                    IsMinion = em.IsMinion,
                    Xp = em.Monster.ChallengeRating.XP,
                    DisplayName = em.Monster.DisplayName,
                    Dexterity = em.Monster.Dexterity.Score,
                    InitiativeBonus = em.Monster.BonusInitiative,
                    ArmorClass = em.Monster.ArmorClass,
                    HitPoints = em.Monster.HitPoints
                })
            })
            .Single(x => x.EncounterId == id);
    }

    public EncounterDto Add(string name, int campaignId)
    {
        var newEncounter = new Encounter
        {
            Name = name,
            Music = dbContext.Find<Music>(1)!,
            Campaign = dbContext.Campaigns.Find(campaignId)!
        };

        dbContext.Add(newEncounter);
        dbContext.SaveChanges();

        return FindDto(newEncounter.Id);
    }

    public EncounterDto Update(EncounterDto encounterDto)
    {
        var encounter = dbContext.Encounters
            .Include(x => x.EncounterMonsters)
                .ThenInclude(x => x.Monster)
            .Single(x => x.Id == encounterDto.EncounterId);

        dbContext.Entry(encounter).CurrentValues.SetValues(encounterDto);

        var encounterMonsters = encounter.EncounterMonsters.GroupBy(x => x.Monster.Id);
        var encounterMonsterDtos = encounterDto.EncounterMonsters.GroupBy(x => x.MonsterId);

        foreach (var encounterMonsterDto in encounterMonsterDtos)
        {
            var monsters = encounterMonsters.SingleOrDefault(x => x.Key == encounterMonsterDto.Key) ?? Enumerable.Empty<EncounterMonster>();
            var encounterCount = monsters.Count();
            var encounterDtoCount = encounterMonsterDto.Count();
            if (encounterCount > encounterDtoCount)
            {
                //remove from encounter until they match
                var toBeRemoved = monsters.Skip(encounterDtoCount).ToList();
                monsters = monsters.Take(encounterDtoCount);
                dbContext.RemoveRange(toBeRemoved);
            }
            else if (encounterCount < encounterDtoCount)
            {
                //add until they match
                var toBeAdded = encounterMonsterDto.Skip(encounterCount).Select(x => new EncounterMonster
                {
                    Quantity = x.Quantity,
                    InitiativeRoll = x.InitiativeRoll,
                    Monster = dbContext.Monsters.Find(x.MonsterId)!,
                    Encounter = encounter
                }).ToList();
                monsters = monsters.Concat(toBeAdded);
                dbContext.AddRange(toBeAdded);
            }
            else
            {
                // counts match, do not add or remove
            }

            //update
            for (int i = 0; i < encounterDtoCount; i++)
            {
                var toBeUpdated = monsters.Skip(i).First();
                var newValues = encounterMonsterDto.Skip(i).First();
                dbContext.Entry(toBeUpdated).CurrentValues.SetValues(newValues);
            }
        }

        foreach (var encounterMonster in encounterMonsters.Where(x => !encounterMonsterDtos.Select(x => x.Key).Contains(x.Key)).SelectMany(x => x))
        {
            dbContext.RemoveRange(encounterMonster);
        }

        dbContext.SaveChanges();

        return FindDto(encounterDto.EncounterId);
    }

    public void Archive(int id)
    {
        var encounter = dbContext.Encounters.Find(id)!;
        encounter.IsArchived = true;

        dbContext.SaveChanges();
    }

    public void Unarchive(int id)
    {
        var encounter = dbContext.Encounters.Find(id)!;
        encounter.IsArchived = false;

        dbContext.SaveChanges();
    }

    public List<EncounterXpThreshold> XpThresholds() => dbContext.EncounterXpThresholds.OrderBy(x => x.Id).ToList();
}
