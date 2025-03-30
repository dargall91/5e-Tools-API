using _5eTools.Data;
using _5eTools.Data.Entities;
using _5eTools.Services.DTOs;
using Microsoft.EntityFrameworkCore;
namespace _5eTools.Services;

public interface IEncounterService
{
    bool EncounterIdExists(int id);
    bool EncounterNameExists(string name, int campaignId);
    EncounterDto FindDto(int id);
    (EncounterDto, int) Add(string name, int campaignId);
    EncounterDto Update(int id, EncounterDto encounterDto);
    void Archive(int id);
    void Unarchive(int id);
}

public class EncounterService(ToolsDbContext dbContext) : IEncounterService
{
    public bool EncounterIdExists(int id) => dbContext.Encounters.Find(id) != default;

    public bool EncounterNameExists(string name, int campaignId)
        => dbContext.Encounters.Include(x => x.Campaign).Any(x => x.Campaign.Id == campaignId && x.Name == name);

    public EncounterDto FindDto(int id)
    {
        return dbContext.Encounters
            .Include(x => x.Music)
            .Include(x => x.EncounterMonsters)
            .ThenInclude(x => x.Monster)
            .Where(x => x.Id == id)
            .Select(x => new EncounterDto
            {
                Name = x.Name,
                HasLairAction = x.HasLairAction,
                MusicId = x.Music.Id,
                EncounterMonsterDtos = x.EncounterMonsters.Select(y => new EncounterMonsterDto
                {
                    MonsterId = y.Monster.Id,
                    Name = y.Monster.Name,
                    Quantity = y.Quantity,
                    InitiativeRoll = y.InitiativeRoll,
                    IsInvisible = y.IsInvisible,
                    IsReinforcement = y.IsReinforcement,
                    IsMinion = y.IsMinion
                })
            })
            .Single();
    }

    public (EncounterDto, int) Add(string name, int campaignId)
    {
        var newEncounter = new Encounter
        {
            Name = name,
            Music = dbContext.Find<Music>(1)!,
            Campaign = dbContext.Campaigns.Find(campaignId)!
        };

        dbContext.Add(newEncounter);
        dbContext.SaveChanges();

        return (FindDto(newEncounter.Id), newEncounter.Id);
    }

    public EncounterDto Update(int id, EncounterDto encounterDto)
    {
        var encounter = dbContext.Encounters
            .Include(x => x.EncounterMonsters)
            .ThenInclude(x => x.Monster)
            .Single(x => x.Id == id);

        dbContext.Entry(encounter).CurrentValues.SetValues(encounterDto);

        var encounterMonsters = encounter.EncounterMonsters.GroupBy(x => x.Monster.Id);
        var encounterMonsterDtos = encounterDto.EncounterMonsterDtos.GroupBy(x => x.MonsterId);

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

        return FindDto(id);
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
}
