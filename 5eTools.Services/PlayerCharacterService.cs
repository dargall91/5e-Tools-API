using _5eTools.Data;
using _5eTools.Data.Constants;
using _5eTools.Data.Entities;
using _5eTools.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace _5eTools.Services;

public interface IPlayerCharacterService
{
    bool PcExists(int id);
    PlayerCharacterDto FindDto(int id);
    List<PlayerCharacterDto> GetByCampaignAndUser(int campaignId, int userId, bool isDead);
    PlayerCharacterDto Add(PlayerCharacterDto pcDto, Campaign campaign, User user);
    PlayerCharacterDto Update(PlayerCharacterDto pcDto);
    PlayerCharacterDto UpdateBase(PlayerCharacterDto pcDto);
    void Kill(int id);
    void Revive(int id);
    StressDto UpdateStress(int pcId, StressDto stressDto);
    PlayerCharacterDto LongRest(int id, int extendedRestDuration);
    List<PlayerCharacterCombatantDto> GetCombatantData(int campaignId);
    PlayerCharacterCombatantDto UpdateCombatantData(PlayerCharacterCombatantDto pcDto);
    PlayerCharacterMasterData MasterData(int campaignId);
    IEnumerable<Item> GetItems();
    Item? AddItem(string name);
}

public class PlayerCharacterService(ToolsDbContext dbContext) : IPlayerCharacterService
{
    private const int BaseSaveDc = 8;
    private const int MeditationDiceRegained = 5;
    private const int BaseStressThreshold = 100;
    private const int StressThresholdModifier = 5;

    public bool PcExists(int id) => dbContext.PlayerCharacters.Find(id) != default;

    public PlayerCharacterDto FindDto(int id)
    {
        var pc = dbContext.PlayerCharacters
            .Include(x => x.Strength)
            .Include(x => x.Dexterity)
            .Include(x => x.Constitution)
            .Include(x => x.Intelligence)
            .Include(x => x.Wisdom)
            .Include(x => x.Charisma)
            .Include(x => x.Resolve)
            .Include(x => x.Stress)
                .ThenInclude(x => x!.StressStatus)
                    .ThenInclude(x => x!.StressType)
            .Include(x => x.CharacterClasses)
                .ThenInclude(x => x.PrimalCompanion)
                    .ThenInclude(x => x!.PrimalCompanionType)
            .Include(x => x.CharacterClasses)
                .ThenInclude(x => x.Subclass)
                .ThenInclude(x => x.Class)
                .ThenInclude(x => x.CasterType)
            .Include(x => x.UsedSpellSlots)
            .Include(x => x.ProficiencyBonus)
            .Include(x => x.ExhaustionLevel)
            .Include(x => x.SpellSlots)
            .Include(x => x.WarlockSpellSlots)
            .Include(x => x.Currency)
            .Include(x => x.InventoryItems)
                .ThenInclude(x => x.Item)
            .Select(PlayerCharacterToDto)
            .Single(x => x.PlayerCharacterId == id);

        return pc;
    }

    public List<PlayerCharacterDto> GetByCampaignAndUser(int campaignId, int userId, bool isDead)
    {
        return dbContext.PlayerCharacters
            .Include(x => x.Strength)
            .Include(x => x.Dexterity)
            .Include(x => x.Constitution)
            .Include(x => x.Intelligence)
            .Include(x => x.Wisdom)
            .Include(x => x.Charisma)
            .Include(x => x.Resolve)
            .Include(x => x.Stress)
                .ThenInclude(x => x!.StressStatus)
                .ThenInclude(x => x!.StressType)
            .Include(x => x.CharacterClasses)
                .ThenInclude(x => x.PrimalCompanion)
                    .ThenInclude(x => x!.PrimalCompanionType)
            .Include(x => x.CharacterClasses)
                .ThenInclude(x => x.Subclass)
                .ThenInclude(x => x.Class)
                .ThenInclude(x => x.CasterType)
            .Include(x => x.UsedSpellSlots)
            .Include(x => x.Campaign)
            .Include(x => x.User)
            .Include(x => x.ProficiencyBonus)
            .Include(x => x.ExhaustionLevel)
            .Include(x => x.SpellSlots)
            .Include(x => x.WarlockSpellSlots)
            .Include(x => x.Currency)
            .Include(x => x.InventoryItems)
                .ThenInclude(x => x.Item)
            .Where(x => x.Campaign.Id == campaignId && x.User.Id == userId && x.IsDead == isDead)
            .Select(PlayerCharacterToDto)
            .OrderBy(x => x.Name)
            .ToList();
    }

    public PlayerCharacterDto Add(PlayerCharacterDto pcDto, Campaign campaign, User user)
    {
        var newPc = new PlayerCharacter
        {
            Name = pcDto.Name,
            Campaign = campaign,
            User = user,
            ProficiencyBonus = dbContext.ProficiencyBonuses.Find(pcDto.CharacterClasses.Sum(x => x.Level))!
        };

        //create stress/resolve if used by campaign
        if (campaign.UsesStress)
        {
            newPc.Stress = new()
            {
                StressThreshold = BaseStressThreshold + (CalculateAbilityScoreModifier(pcDto.Resolve!.Score) * StressThresholdModifier),
                StressMaximum = BaseStressThreshold * 2
            };

            newPc.Resolve = new()
            {
                Score = pcDto.Resolve!.Score
            };
        }

        dbContext.Entry(newPc).CurrentValues.SetValues(pcDto);
        UpdateAbilityScores(newPc, pcDto);
        UpdateCharacterClasses(newPc, pcDto.CharacterClasses);
        UpdateMaxHitPoints(newPc);
        UpdateCasterLevels(newPc);

        pcDto.HitPointMaximum = newPc.HitPointMaximum;
        dbContext.Add(newPc);
        dbContext.SaveChanges();

        return FindDto(newPc.Id);
    }

    public PlayerCharacterDto Update(PlayerCharacterDto pcDto)
    {
        var toBeUpdated = dbContext.PlayerCharacters
            .Include(x => x.Strength)
            .Include(x => x.Dexterity)
            .Include(x => x.Constitution)
            .Include(x => x.Intelligence)
            .Include(x => x.Wisdom)
            .Include(x => x.Charisma)
            .Include(x => x.Resolve)
            .Include(x => x.Stress)
                .ThenInclude(x => x!.StressStatus)
                    .ThenInclude(x => x!.StressType)
            .Include(x => x.CharacterClasses)
                .ThenInclude(x => x.PrimalCompanion)
                    .ThenInclude(x => x!.PrimalCompanionType)
            .Include(x => x.CharacterClasses)
                .ThenInclude(x => x.Subclass)
                .ThenInclude(x => x.Class)
                .ThenInclude(x => x.CasterType)
            .Include(x => x.UsedSpellSlots)
            .Include(x => x.Campaign)
            .Include(x => x.ProficiencyBonus)
            .Include(x => x.ExhaustionLevel)
            .Include(x => x.SpellSlots)
            .Include(x => x.WarlockSpellSlots)
            .Include(x => x.Currency)
            .Include(x => x.InventoryItems)
                .ThenInclude(x => x.Item)
            .Single(x => x.Id == pcDto.PlayerCharacterId);

        dbContext.Entry(toBeUpdated).CurrentValues.SetValues(pcDto);
        UpdateAbilityScores(toBeUpdated, pcDto);
        UpdateCharacterClasses(toBeUpdated, pcDto.CharacterClasses);
        UpdateMaxHitPoints(toBeUpdated);
        UpdateCasterLevels(toBeUpdated);
        dbContext.Entry(toBeUpdated.Currency).CurrentValues.SetValues(pcDto.Currency);

        if (pcDto.UsedSpellSlots != default)
        {
            dbContext.Entry(toBeUpdated.UsedSpellSlots!).CurrentValues.SetValues(pcDto.UsedSpellSlots);
        }

        if (toBeUpdated.Stress != default)
        {
            toBeUpdated.Stress.StressThreshold = BaseStressThreshold + (CalculateAbilityScoreModifier(pcDto.Resolve!.Score) * StressThresholdModifier);
            toBeUpdated.Stress.StressMaximum = toBeUpdated.Stress.StressThreshold * 2;
        }

        toBeUpdated.ProficiencyBonus = dbContext.ProficiencyBonuses.Find(toBeUpdated.CharacterClasses.Sum(x => x.Level))!;
        toBeUpdated.ExhaustionLevel = dbContext.ExhaustionLevels.Find(pcDto.ExhaustionLevel?.Id);

        UpdateInventoryItems(toBeUpdated, pcDto.InventoryItems);

        dbContext.SaveChanges();

        return PlayerCharacterToDto(toBeUpdated);
    }

    public PlayerCharacterDto UpdateBase(PlayerCharacterDto pcDto)
    {
        var toBeUpdated = dbContext.PlayerCharacters.Find(pcDto.PlayerCharacterId)!;

        dbContext.Entry(toBeUpdated).CurrentValues.SetValues(pcDto);
        dbContext.SaveChanges();

        return pcDto;
    }

    public void Kill(int id)
    {
        var pc = dbContext.PlayerCharacters.Find(id)!;
        pc.IsDead = true;

        dbContext.SaveChanges();
    }

    public void Revive(int id)
    {
        var pc = dbContext.PlayerCharacters.Find(id)!;
        pc.IsDead = false;

        dbContext.SaveChanges();
    }

    public StressDto UpdateStress(int pcId, StressDto stressDto)
    {
        var stress = dbContext.PlayerCharacters
            .Include(x => x.Stress)
                .ThenInclude(x => x!.StressStatus)
            .Where(x => x.Id == pcId)
            .Select(x => x.Stress)
            .SingleOrDefault();

        if (stress != default)
        {
            stress.StressLevel = stressDto.StressLevel;
            stress.MeditationDiceUsed = stressDto.MeditationDiceUsed;
            stress.StressStatus = stressDto.StressStatus == null ? null : dbContext.StressStatuses.Find(stressDto.StressStatus.Id);
            //preserve db values of threshold/max since they are not manually set
            stressDto.StressThreshold = stress.StressThreshold;
            stressDto.StressMaximum = stress.StressMaximum;

            dbContext.SaveChanges();
        }

        return stressDto;
    }

    public PlayerCharacterDto LongRest(int id, int extendedRestDuration)
    {
        var pc = dbContext.PlayerCharacters
            .Include(x => x.Stress)
                .ThenInclude(x => x!.StressStatus)
                    .ThenInclude(x => x!.StressType)
            .Include(x => x.ExhaustionLevel)
            .Include(x => x.CharacterClasses)
                .ThenInclude(x => x.PrimalCompanion)
            .Include(x => x.CharacterClasses)
                .ThenInclude(x => x.Subclass)
                .ThenInclude(x => x.Class)
            .Include(x => x.UsedSpellSlots)
            .Single(x => x.Id == id);

        pc.Damage = 0;
        pc.TemporaryHitPoints = 0;

        if (pc.ExhaustionLevel != null)
        {
            var newlevel = pc.ExhaustionLevel.Id - (1 + extendedRestDuration);
            pc.ExhaustionLevel = newlevel > 1 ? dbContext.ExhaustionLevels.Find(newlevel) : null;
        }

        if (pc.UsedSpellSlots != default)
        {
            pc.UsedSpellSlots.FirstLevel = 0;
            pc.UsedSpellSlots.SecondLevel = 0;
            pc.UsedSpellSlots.ThirdLevel = 0;
            pc.UsedSpellSlots.FourthLevel = 0;
            pc.UsedSpellSlots.FifthLevel = 0;
            pc.UsedSpellSlots.SixthLevel = 0;
            pc.UsedSpellSlots.SeventhLevel = 0;
            pc.UsedSpellSlots.EigthLevel = 0;
            pc.UsedSpellSlots.NinthLevel = 0;
            pc.UsedSpellSlots.Warlock = 0;
        }

        var totalLevels = pc.CharacterClasses.Sum(x => x.Level);
        var hitDiceToRegain = (int) Math.Round(totalLevels / 2.0, MidpointRounding.ToNegativeInfinity);
        //minimum of one die will be regained
        if (hitDiceToRegain < 1)
        {
            hitDiceToRegain = 1;
        }
        var totalRegained = 0;

        //optimize hit dice regained by prioritizing largest hit dice first
        foreach (var characterClass in pc.CharacterClasses.OrderByDescending(x => x.Subclass.Class.HitDieSize))
        {
            //skip if the total dice regained has reached the maximum allowed or if the class has not spent any hit dice
            if (totalRegained < hitDiceToRegain && characterClass.HitDiceUsed > 0)
            {
                //if the amount used is less than the remaining amount that can be regained, regain all used dice
                //otherwise regain as many as possible
                if (characterClass.HitDiceUsed <= (hitDiceToRegain - totalRegained))
                {
                    totalRegained += characterClass.HitDiceUsed;
                    characterClass.HitDiceUsed = 0;
                }
                else
                {
                    characterClass.HitDiceUsed -= hitDiceToRegain - totalRegained;
                    totalRegained = hitDiceToRegain;
                }
            }

            if (characterClass.PrimalCompanion != default)
            {
                characterClass.PrimalCompanion.Damage = 0;
                characterClass.PrimalCompanion.TemporaryHitPoints = 0;
                var companionDiceToRegain = (int) Math.Round(characterClass.Level / 2.0, MidpointRounding.ToNegativeInfinity);
                //minimum of one die will be regained
                if (companionDiceToRegain < 1)
                {
                    companionDiceToRegain = 1;
                }
                //if the companion has used less dice than what can be regained, regain them all
                if (characterClass.PrimalCompanion.HitDiceUsed < companionDiceToRegain)
                {
                    characterClass.PrimalCompanion.HitDiceUsed = 0;
                }
                else
                {
                    characterClass.PrimalCompanion.HitDiceUsed -= companionDiceToRegain;
                }
            }
        }

        if (pc.Stress != default)
        {
            //if less dice than what will be regained have been used, regain them all
            if (pc.Stress.MeditationDiceUsed < MeditationDiceRegained)
            {
                pc.Stress.MeditationDiceUsed = 0;
            }
            else
            {
                pc.Stress.MeditationDiceUsed -= MeditationDiceRegained;
            }

            //if stress is above threshold, it becomes the threshold
            //otherwise lose 50. If currently at 50 or less, lose it all
            if (pc.Stress.StressLevel > pc.Stress.StressThreshold)
            {
                pc.Stress.StressLevel = pc.Stress.StressThreshold;
            }
            else if (pc.Stress.StressLevel > 50)
            {
                pc.Stress.StressLevel -= 50;
            }
            else
            {
                pc.Stress.StressLevel = 0;
            }

            //if the character has a virtue, or if they have an affliction and their stress level is now half of their stress threshold or less, they lose it
            if (pc.Stress.StressStatus != default
                && (pc.Stress.StressStatus.StressType.Id == StressTypes.Virtue
                    || pc.Stress.StressLevel <= (pc.Stress.StressThreshold / 2)))
            {
                pc.Stress.StressStatus = null;
            }
        }

        dbContext.SaveChanges();

        return FindDto(id);
    }

    public List<PlayerCharacterCombatantDto> GetCombatantData(int campaignId)
    {
        return dbContext.PlayerCharacters
            .Include(x => x.Campaign)
            .Where(x => !x.IsDead && x.Campaign.Id == campaignId)
            .Select(x => new PlayerCharacterCombatantDto
            {
                PlayerCharacterId = x.Id,
                PlayerCharacterName = x.Name,
                TotalArmorClass = x.BaseArmorClass + x.ArmorClassBonus,
                IsCombatant = x.IsCombatant,
                InitiativeRoll = x.InitiativeRoll,
                InitiativeBonus = x.InitiativeBonus
            })
            .OrderBy(x => x.PlayerCharacterName)
            .ToList();
    }

    public PlayerCharacterCombatantDto UpdateCombatantData(PlayerCharacterCombatantDto pcDto)
    {
        var toBeUpdated = dbContext.PlayerCharacters.Find(pcDto.PlayerCharacterId)!;

        dbContext.Entry(toBeUpdated).CurrentValues.SetValues(pcDto);
        dbContext.SaveChanges();

        //get the latest AC/Name (though name should not change)
        pcDto.PlayerCharacterName = toBeUpdated.Name;
        pcDto.TotalArmorClass = toBeUpdated.BaseArmorClass + toBeUpdated.ArmorClassBonus;

        return pcDto;
    }

    public PlayerCharacterMasterData MasterData(int campaignId)
    {
        return new PlayerCharacterMasterData
        {
            StressTypes = dbContext.StressTypes
                .Include(x => x.StressStatuses)
                .Select(st => new StressTypeDto
                {
                    Name = st.Name,
                    MinimumRoll = st.MinimumRoll,
                    MaximumRoll = st.MaximumRoll,
                    StressStatuses = st.StressStatuses.Select(ss => new StressStatusDto
                    {
                        Id = ss.Id,
                        Name = ss.Name,
                        Description = ss.Description,
                        StressType = st.Name,
                        Roll = ss.Roll
                    })
                })
                .ToList(),
            ExhaustionLevels = dbContext.ExhaustionLevels.ToList(),
            PrimalCompanionTypes = dbContext.PrimalCompanionTypes.ToList()
        };
    }

    public IEnumerable<Item> GetItems() => dbContext.Items.OrderBy(x => x.Name).ToList().Prepend(new Item { Id = 0, Name = "New Item" });

    public Item? AddItem(string name)
    {
        Item? newItem = null;

        if (!dbContext.Items.Any(x => x.Name == name))
        {
            newItem = new Item { Name = name };

            dbContext.Add(newItem);
            dbContext.SaveChanges();
        }

        return newItem;
    }

    private PlayerCharacterDto PlayerCharacterToDto(PlayerCharacter pc)
    {
        return new PlayerCharacterDto
        {
            PlayerCharacterId = pc.Id,
            Name = pc.Name,
            BaseArmorClass = pc.BaseArmorClass,
            ArmorClassBonus = pc.ArmorClassBonus,
            Damage = pc.Damage,
            HitPointMaximum = pc.HitPointMaximum,
            TemporaryHitPoints = pc.TemporaryHitPoints,
            MaxHitPointReduction = pc.MaxHitPointReduction,
            DeathSaveFailures = pc.DeathSaveFailures,
            DeathSaveSuccesses = pc.DeathSaveSuccesses,
            ToughFeat = pc.ToughFeat,
            ExhaustionLevel = pc.ExhaustionLevel,
            SpellSlots = pc.SpellSlots,
            WarlockSpellSlots = pc.WarlockSpellSlots,
            ProficiencyBonus = pc.ProficiencyBonus,
            Strength = new StrengthDto
            {
                Score = pc.Strength.Score,
                Proficient = pc.Strength.Proficient,
                Athletics = pc.Strength.Athletics
            },
            Dexterity = new DexterityDto
            {
                Score = pc.Dexterity.Score,
                Proficient = pc.Dexterity.Proficient,
                Acrobatics = pc.Dexterity.Acrobatics,
                SleightOfHand = pc.Dexterity.SleightOfHand,
                Stealth = pc.Dexterity.Stealth
            },
            Constitution = new ConstitutionDto
            {
                Score = pc.Constitution.Score,
                Proficient = pc.Constitution.Proficient
            },
            Intelligence = new IntelligenceDto
            {
                Score = pc.Intelligence.Score,
                Proficient = pc.Intelligence.Proficient,
                Arcana = pc.Intelligence.Arcana,
                History = pc.Intelligence.History,
                Investigation = pc.Intelligence.Investigation,
                Nature = pc.Intelligence.Nature,
                Religion = pc.Intelligence.Religion
            },
            Wisdom = new WisdomDto
            {
                Score = pc.Wisdom.Score,
                Proficient = pc.Wisdom.Proficient,
                AnimalHandling = pc.Wisdom.AnimalHandling,
                Insight = pc.Wisdom.Insight,
                Medicine = pc.Wisdom.Medicine,
                Perception = pc.Wisdom.Perception,
                Survival = pc.Wisdom.Survival
            },
            Charisma = new CharismaDto
            {
                Score = pc.Charisma.Score,
                Proficient = pc.Charisma.Proficient,
                Deception = pc.Charisma.Deception,
                Intimidation = pc.Charisma.Intimidation,
                Performance = pc.Charisma.Performance,
                Persuasion = pc.Charisma.Persuasion
            },
            Resolve = pc.Resolve == default
                ? null
                : new ResolveDto
                {
                    Score = pc.Resolve.Score
                },
            Stress = pc.Stress == default
                ? null
                : new StressDto
                {
                    StressLevel = pc.Stress.StressLevel,
                    StressThreshold = pc.Stress.StressThreshold,
                    StressMaximum = pc.Stress.StressMaximum,
                    MeditationDiceUsed = pc.Stress.MeditationDiceUsed,
                    StressStatus = pc.Stress.StressStatus == default
                        ? null
                        : new StressStatusDto
                        {
                            Id = pc.Stress.StressStatus.Id,
                            Roll = pc.Stress.StressStatus.Roll,
                            Name = pc.Stress.StressStatus.Name,
                            Description = pc.Stress.StressStatus.Description,
                            StressType = pc.Stress.StressStatus.StressType.Name,
                        }
                },
            UsedSpellSlots = pc.UsedSpellSlots == default
                ? null
                : new UsedSpellSlotsDto
                {
                    FirstLevel = pc.UsedSpellSlots.FirstLevel,
                    SecondLevel = pc.UsedSpellSlots.SecondLevel,
                    ThirdLevel = pc.UsedSpellSlots.ThirdLevel,
                    FourthLevel = pc.UsedSpellSlots.FourthLevel,
                    FifthLevel = pc.UsedSpellSlots.FifthLevel,
                    SixthLevel = pc.UsedSpellSlots.SixthLevel,
                    SeventhLevel = pc.UsedSpellSlots.SeventhLevel,
                    EigthLevel = pc.UsedSpellSlots.EigthLevel,
                    NinthLevel = pc.UsedSpellSlots.NinthLevel,
                    Warlock = pc.UsedSpellSlots.Warlock
                },
            CharacterClasses = pc.CharacterClasses.Select(x => new CharacterClassDto
            {
                Level = x.Level,
                HitDiceUsed = x.HitDiceUsed,
                BaseClass = x.BaseClass,
                ClassSaveDc = x.BaseClassSaveDc,
                Subclass = new SubclassDto
                {
                    Id = x.Subclass.Id,
                    Name = x.Subclass.Name,
                    ClassName = x.Subclass.Class.Name,
                    PrimalCompanion = x.Subclass.PrimalCompanion,
                    ClassHitDieSize = x.Subclass.Class.HitDieSize,
                    JackOfAllTrades = x.Subclass.Class.Id == Classes.Bard && x.Level > 1
                },
                PrimalCompanion = x.PrimalCompanion == default
                    ? null
                    : new PrimalCompanionDto
                    {
                        Name = x.PrimalCompanion.Name,
                        HitPointMaximum = x.PrimalCompanion.HitPointMaximum,
                        ArmorClassBonus = x.PrimalCompanion.ArmorClassBonus,
                        Damage = x.PrimalCompanion.Damage,
                        MaxHitPointReduction = x.PrimalCompanion.MaxHitPointReduction,
                        TemporaryHitPoints = x.PrimalCompanion.TemporaryHitPoints,
                        DeathSaveFailures = x.PrimalCompanion.DeathSaveFailures,
                        DeathSaveSuccesses = x.PrimalCompanion.DeathSaveSuccesses,
                        HitDiceUsed = x.PrimalCompanion.HitDiceUsed,
                        StrengthOverride = x.PrimalCompanion.StrengthOverride,
                        DexterityOverride = x.PrimalCompanion.DexterityOverride,
                        ConstitutionOverride = x.PrimalCompanion.StrengthOverride,
                        IntelligenceOverride = x.PrimalCompanion.IntelligenceOverride,
                        WisdomOverride = x.PrimalCompanion.WisdomOverride,
                        CharismaOverride = x.PrimalCompanion.CharismaOverride,
                        PrimalCompanionType = x.PrimalCompanion.PrimalCompanionType
                    }
            }),
            Currency = new CurrencyDto
            {
                Copper = pc.Currency.Copper,
                Silver = pc.Currency.Silver,
                Electrum = pc.Currency.Electrum,
                Gold = pc.Currency.Gold,
                Platinum = pc.Currency.Platinum
            },
            InventoryItems = pc.InventoryItems.Select(x => new InventoryItemDto
            {
                ItemId = x.Item.Id,
                Name = x.Item.Name,
                Quantity = x.Quantity
            }).OrderBy(x => x.Name)
        };
    }

    private void UpdateAbilityScores(PlayerCharacter pc, PlayerCharacterDto pcDto)
    {
        dbContext.Entry(pc.Strength).CurrentValues.SetValues(pcDto.Strength);
        dbContext.Entry(pc.Dexterity).CurrentValues.SetValues(pcDto.Dexterity);
        dbContext.Entry(pc.Constitution).CurrentValues.SetValues(pcDto.Constitution);
        dbContext.Entry(pc.Intelligence).CurrentValues.SetValues(pcDto.Intelligence);
        dbContext.Entry(pc.Wisdom).CurrentValues.SetValues(pcDto.Wisdom);
        dbContext.Entry(pc.Charisma).CurrentValues.SetValues(pcDto.Charisma);

        //pcDto resolve will be null if campaign does not use stress
        if (pcDto.Resolve != default)
        {
            dbContext.Entry(pc.Resolve!).CurrentValues.SetValues(pcDto.Resolve);
        }
    }

    private void AddNewClassLevels(PlayerCharacter pc, IEnumerable<CharacterClassDto> characterClassDtos)
    {
        var currentSubclassIds = pc.CharacterClasses.Select(x => x.Subclass.Id);
        var newSubclassIds = characterClassDtos
            .Select(cc => cc.Subclass.Id)
            .Where(x => !currentSubclassIds.Contains(x));

        var newSubclasses = dbContext.Subclasses
            .Include(x => x.Class)
            .ThenInclude(x => x.CasterType)
            .Where(s => newSubclassIds.Contains(s.Id))
            .Select(x => new CharacterClass
            {
                Subclass = x,
                PrimalCompanion = x.PrimalCompanion
                    ? new PrimalCompanion
                    {
                        Name = "Primal Companion",
                        PrimalCompanionType = dbContext.Find<PrimalCompanionType>(1)!
                    }
                    : null
            })
            .ToList();

        pc.CharacterClasses = pc.CharacterClasses.Concat(newSubclasses).ToList();
    }

    private void UpdateCharacterClasses(PlayerCharacter pc, IEnumerable<CharacterClassDto> characterClassDtos)
    {
        AddNewClassLevels(pc, characterClassDtos);

        foreach (var characterClass in pc.CharacterClasses)
        {
            var characterClassDto = characterClassDtos.Single(x => x.Subclass.Id == characterClass.Subclass.Id);
            dbContext.Entry(characterClass).CurrentValues.SetValues(characterClassDto);

            //set primal companion, if used by sublcass
            if (characterClass.PrimalCompanion != null)
            {
                //Primal Companion DTO will be null if this is character creation, nothing to do except set max hit points
                if (characterClassDto.PrimalCompanion != null)
                {
                    var companionDto = characterClassDto.PrimalCompanion!;
                    dbContext.Entry(characterClass.PrimalCompanion).CurrentValues.SetValues(companionDto);

                    characterClass.PrimalCompanion.PrimalCompanionType = dbContext.Find<PrimalCompanionType>(companionDto.PrimalCompanionType.Id)!;
                }

                characterClass.PrimalCompanion.HitPointMaximum = characterClass.PrimalCompanion.PrimalCompanionType.HitPointMultiplier * (characterClass.Level + 1);
            }

            SetClassSaveDc(pc, characterClass);
        }
    }

    private static void SetClassSaveDc(PlayerCharacter pc, CharacterClass characterClass)
    {
        characterClass.BaseClassSaveDc = BaseSaveDc;

        if (characterClass.Subclass.Class.ClassAbilityScore == "Strength")
        {
            characterClass.BaseClassSaveDc += CalculateAbilityScoreModifier(pc.Strength.Score);
        }
        else if (characterClass.Subclass.Class.ClassAbilityScore == "Dexterity")
        {
            characterClass.BaseClassSaveDc += CalculateAbilityScoreModifier(pc.Dexterity.Score);
        }
        else if (characterClass.Subclass.Class.ClassAbilityScore == "Constitution")
        {
            characterClass.BaseClassSaveDc += CalculateAbilityScoreModifier(pc.Constitution.Score);
        }
        else if (characterClass.Subclass.Class.ClassAbilityScore == "Intelligence")
        {
            characterClass.BaseClassSaveDc += CalculateAbilityScoreModifier(pc.Intelligence.Score);
        }
        else if (characterClass.Subclass.Class.ClassAbilityScore == "Wisdom")
        {
            characterClass.BaseClassSaveDc += CalculateAbilityScoreModifier(pc.Wisdom.Score);
        }
        else if (characterClass.Subclass.Class.ClassAbilityScore == "Charisma")
        {
            characterClass.BaseClassSaveDc += CalculateAbilityScoreModifier(pc.Charisma.Score);
        }
        else
        {
            // do nothing, should never reach this point
        }
    }

    private static void UpdateMaxHitPoints(PlayerCharacter pc)
    {
        var conModifer = CalculateAbilityScoreModifier(pc.Constitution.Score);
        var totalLevels = pc.CharacterClasses.Sum(x => x.Level);

        var newMaximum = 0;

        foreach (var characterClass in pc.CharacterClasses)
        {
            if (characterClass.BaseClass)
            {
                newMaximum += characterClass.Subclass.Class.HitDieSize + conModifer;
                newMaximum += (characterClass.Level - 1) * (characterClass.Subclass.Class.AverageHitDieRoll + conModifer);
            }
            else
            {
                newMaximum += characterClass.Level * (characterClass.Subclass.Class.AverageHitDieRoll + conModifer);
            }
        }

        newMaximum = pc.Campaign.UsesInflatedHitPoints
            ? (int) Math.Round(newMaximum * 1.5, MidpointRounding.ToPositiveInfinity)
            : newMaximum;

        if (pc.DwarvenToughness)
        {
            newMaximum += totalLevels;
        }

        if (pc.ToughFeat)
        {
            newMaximum += totalLevels * 2;
        }

        pc.HitPointMaximum = newMaximum;
    }

    private void UpdateCasterLevels(PlayerCharacter pc)
    {
        var spellcasterClasses = pc.CharacterClasses.Where(x => x.Subclass.ThirdCaster || x.Subclass.Class.CasterType.Id != CasterTypes.NonCaster);
        //warlock levels don't count towards multiclassing when calculating spellcaster level
        var isMulticlassedSpellcaster = spellcasterClasses.Count(x => x.Subclass.Class.CasterType.Id != CasterTypes.Warlock) > 1;

        var thirdCasterLevels = spellcasterClasses.Where(x => x.Subclass.ThirdCaster).Sum(x => x.Level);
        var halfCasterLevels = spellcasterClasses.Where(x => x.Subclass.Class.CasterType.Id == CasterTypes.HalfCaster).Sum(x => x.Level);
        var fullCasterLevels = spellcasterClasses.Where(x => x.Subclass.Class.CasterType.Id == CasterTypes.FullCaster).Sum(x => x.Level);
        var artificerLevels = spellcasterClasses.SingleOrDefault(x => x.Subclass.Class.CasterType.Id == CasterTypes.Artficer)?.Level ?? 0;

        var spellcasterLevel = CalculateSubclassCasterLevel(thirdCasterLevels, 3.0, isMulticlassedSpellcaster)
            + CalculateSubclassCasterLevel(halfCasterLevels, 2.0, isMulticlassedSpellcaster)
            + fullCasterLevels
            //multiclassed artificiers and artificers above level 1 gain spellcaster levels at the same rate as other half casters
            //level one non-multiclassed artificiers are a special exception that gain spell slots at level one
            + (isMulticlassedSpellcaster || artificerLevels != 1 ? CalculateSubclassCasterLevel(artificerLevels, 2.0, isMulticlassedSpellcaster) : 1);

        var warlockLevel = spellcasterClasses.SingleOrDefault(x => x.Subclass.Class.CasterType.Id == CasterTypes.Warlock)?.Level ?? 0;

        if (spellcasterLevel > 0 || warlockLevel > 0)
        {
            //create user spell slots if caster levels were just gained, otherwise update
            if (pc.UsedSpellSlots == null)
            {
                pc.UsedSpellSlots = new UsedSpellSlots();
            }
            else
            {
                dbContext.Entry(pc.UsedSpellSlots).CurrentValues.SetValues(pc.UsedSpellSlots);
            }
        }

        pc.SpellSlots = spellcasterLevel > 0 ? dbContext.SpellSlots.Find(spellcasterLevel) : null;
        pc.WarlockSpellSlots = warlockLevel > 0 ? dbContext.WarlockSpellSlots.Find(warlockLevel) : null;
    }

    private static int CalculateAbilityScoreModifier(int score) => (int) Math.Round((score - 10) / 2.0, MidpointRounding.ToNegativeInfinity);

    private static int CalculateSubclassCasterLevel(int level, double divisor, bool isMulticlassed)
        => (int) Math.Round(level / divisor, isMulticlassed ? MidpointRounding.ToNegativeInfinity : MidpointRounding.ToPositiveInfinity);

    private void UpdateInventoryItems(PlayerCharacter pc, IEnumerable<InventoryItemDto> itemDtos)
    {
        //update quanity of all items in both lists
        foreach (var item in pc.InventoryItems.Where(x => itemDtos.Any(y => y.ItemId == x.Item.Id)))
        {
            item.Quantity = itemDtos.Single(x => x.ItemId == item.Item.Id).Quantity;
        }

        //remove all items not in the dto list
        foreach (var item in pc.InventoryItems.Where(x => !itemDtos.Any(y => y.ItemId == x.Item.Id)))
        {
            dbContext.Remove(item);
        }

        //add new items from the dto list
        foreach (var item in itemDtos.Where(x => !pc.InventoryItems.Any(y => y.Item.Id == x.ItemId)))
        {
            pc.InventoryItems.Add(new InventoryItem { PlayerCharacter = pc, Item = dbContext.Items.Find(item.ItemId)!, Quantity = 1 });
        }
    }
}
