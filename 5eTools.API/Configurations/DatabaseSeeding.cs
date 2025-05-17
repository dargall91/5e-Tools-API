using _5eTools.Data;
using _5eTools.Data.Constants;
using _5eTools.Data.Entities;

namespace _5eTools.API.Configurations;

public class DatabaseSeeding(ToolsDbContext dbContext)
{
    public void Seed()
    {
        SeedProficiencyBonuses();
        SeedExhaustionLevels();
        SeedSpellSlots();
        SeedWarlockSpellSlots();
        SeedCasterTypes();
        SeedClasses();
        SeedPrimalCompanionTypes();
        SeedStressTypes();
        SeedStressStatuses();
        SeedChallengeRatings();
        SeedXpThresholds();
        SeedItems();

        dbContext.SaveChanges();
    }

    private void SeedProficiencyBonuses()
    {
        var bonusSeeds = new List<ProficiencyBonus>
        {
            new() { Id = 1, Bonus = 2 },
            new() { Id = 2, Bonus = 2 },
            new() { Id = 3, Bonus = 2 },
            new() { Id = 4, Bonus = 2 },
            new() { Id = 5, Bonus = 3 },
            new() { Id = 6, Bonus = 3 },
            new() { Id = 7, Bonus = 3 },
            new() { Id = 8, Bonus = 3 },
            new() { Id = 9, Bonus = 4 },
            new() { Id = 10, Bonus = 4 },
            new() { Id = 11, Bonus = 4 },
            new() { Id = 12, Bonus = 4 },
            new() { Id = 13, Bonus = 5 },
            new() { Id = 14, Bonus = 5 },
            new() { Id = 15, Bonus = 5 },
            new() { Id = 16, Bonus = 5 },
            new() { Id = 17, Bonus = 6 },
            new() { Id = 18, Bonus = 6 },
            new() { Id = 19, Bonus = 6 },
            new() { Id = 20, Bonus = 6 }
        };

        Seed(bonusSeeds);
    }

    private void SeedExhaustionLevels()
    {
        var exhaustionSeeds = new List<ExhaustionLevel>
        {
            new() { Id = 1, Description = "Disadvantage on ability checks" },
            new() { Id = 2, Description = "Speed halved" },
            new() { Id = 3, Description = "Disadvantage on attack rolls and saving throws" },
            new() { Id = 4, Description = "Hit point maximum halved" },
            new() { Id = 5, Description = "Speed reduced to 0" },
            new() { Id = 6, Description = "Death" }
        };

        Seed(exhaustionSeeds);
    }

    private void SeedSpellSlots()
    {
        var spellSlotsSeeds = new List<SpellSlots>
        {
            new() { Id = 1, FirstLevel = 2, SecondLevel = 0, ThirdLevel = 0, FourthLevel = 0, FifthLevel = 0, SixthLevel = 0, SeventhLevel = 0, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 2, FirstLevel = 3, SecondLevel = 0, ThirdLevel = 0, FourthLevel = 0, FifthLevel = 0, SixthLevel = 0, SeventhLevel = 0, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 3, FirstLevel = 4, SecondLevel = 2, ThirdLevel = 0, FourthLevel = 0, FifthLevel = 0, SixthLevel = 0, SeventhLevel = 0, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 4, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 0, FourthLevel = 0, FifthLevel = 0, SixthLevel = 0, SeventhLevel = 0, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 5, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 2, FourthLevel = 0, FifthLevel = 0, SixthLevel = 0, SeventhLevel = 0, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 6, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 0, FifthLevel = 0, SixthLevel = 0, SeventhLevel = 0, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 7, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 1, FifthLevel = 0, SixthLevel = 0, SeventhLevel = 0, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 8, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 2, FifthLevel = 0, SixthLevel = 0, SeventhLevel = 0, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 9, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 3, FifthLevel = 1, SixthLevel = 0, SeventhLevel = 0, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 10, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 3, FifthLevel = 2, SixthLevel = 0, SeventhLevel = 0, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 11, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 3, FifthLevel = 2, SixthLevel = 1, SeventhLevel = 0, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 12, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 3, FifthLevel = 2, SixthLevel = 1, SeventhLevel = 0, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 13, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 3, FifthLevel = 2, SixthLevel = 1, SeventhLevel = 1, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 14, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 3, FifthLevel = 2, SixthLevel = 1, SeventhLevel = 1, EigthLevel = 0, NinthLevel = 0 },
            new() { Id = 15, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 3, FifthLevel = 2, SixthLevel = 1, SeventhLevel = 1, EigthLevel = 1, NinthLevel = 0 },
            new() { Id = 16, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 3, FifthLevel = 2, SixthLevel = 1, SeventhLevel = 1, EigthLevel = 1, NinthLevel = 0 },
            new() { Id = 17, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 3, FifthLevel = 2, SixthLevel = 1, SeventhLevel = 1, EigthLevel = 1, NinthLevel = 1 },
            new() { Id = 18, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 3, FifthLevel = 3, SixthLevel = 1, SeventhLevel = 1, EigthLevel = 1, NinthLevel = 1 },
            new() { Id = 19, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 3, FifthLevel = 3, SixthLevel = 2, SeventhLevel = 1, EigthLevel = 1, NinthLevel = 1 },
            new() { Id = 20, FirstLevel = 4, SecondLevel = 3, ThirdLevel = 3, FourthLevel = 3, FifthLevel = 3, SixthLevel = 2, SeventhLevel = 2, EigthLevel = 1, NinthLevel = 1 }
        };

        Seed(spellSlotsSeeds);
    }

    private void SeedWarlockSpellSlots()
    {
        var warlockSlotsSeeds = new List<WarlockSpellSlots>
        {
            new() { Id = 1, Level = 1, Slots = 1 },
            new() { Id = 2, Level = 1, Slots = 2 },
            new() { Id = 3, Level = 2, Slots = 2 },
            new() { Id = 4, Level = 2, Slots = 2 },
            new() { Id = 5, Level = 3, Slots = 2 },
            new() { Id = 6, Level = 3, Slots = 2 },
            new() { Id = 7, Level = 4, Slots = 2 },
            new() { Id = 8, Level = 4, Slots = 2 },
            new() { Id = 9, Level = 5, Slots = 2 },
            new() { Id = 10, Level = 5, Slots = 2 },
            new() { Id = 11, Level = 5, Slots = 3 },
            new() { Id = 12, Level = 5, Slots = 3 },
            new() { Id = 13, Level = 5, Slots = 3 },
            new() { Id = 14, Level = 5, Slots = 3 },
            new() { Id = 15, Level = 5, Slots = 3 },
            new() { Id = 16, Level = 5, Slots = 3 },
            new() { Id = 17, Level = 5, Slots = 4 },
            new() { Id = 18, Level = 5, Slots = 4 },
            new() { Id = 19, Level = 5, Slots = 4 },
            new() { Id = 20, Level = 5, Slots = 4 }
        };

        Seed(warlockSlotsSeeds);
    }

    private void SeedCasterTypes()
    {
        var casterTypeSeeds = new List<CasterType>
        {
            new() { Id = CasterTypes.NonCaster, Name = "Non-Caster" },
            new() { Id = CasterTypes.FullCaster, Name = "Full Caster" },
            new() { Id = CasterTypes.HalfCaster, Name = "Half Caster" },
            new() { Id = CasterTypes.Warlock, Name = "Warlock" },
            new() { Id = CasterTypes.Artficer, Name = "Artificer" }
        };

        Seed(casterTypeSeeds);
    }

    private void SeedClasses()
    {
        var casterTypes = dbContext.Set<CasterType>().ToList();
        var classSeeds = new List<Class>
        {
            new() { Id = Classes.Artificer, Name = "Artificer", HitDieSize = 8,  AverageHitDieRoll = 5, ClassAbilityScore = "Intelligence", CasterType = casterTypes.Single(x => x.Id == CasterTypes.Artficer) },
            new() { Id = Classes.Barbarian, Name = "Barbarian", HitDieSize = 12,  AverageHitDieRoll = 7, ClassAbilityScore = "Strength", CasterType = casterTypes.Single(x => x.Id == CasterTypes.NonCaster) },
            new() { Id = Classes.Bard, Name = "Bard", HitDieSize = 8,  AverageHitDieRoll = 5, ClassAbilityScore = "Charisma", CasterType = casterTypes.Single(x => x.Id == CasterTypes.FullCaster) },
            new() { Id = Classes.Cleric, Name = "Cleric", HitDieSize = 8,  AverageHitDieRoll = 5, ClassAbilityScore = "Wisdom", CasterType = casterTypes.Single(x => x.Id == CasterTypes.FullCaster) },
            new() { Id = Classes.Druid, Name = "Druid", HitDieSize = 8,  AverageHitDieRoll = 5, ClassAbilityScore = "Wisdom", CasterType = casterTypes.Single(x => x.Id == CasterTypes.FullCaster) },
            new() { Id = Classes.Fighter, Name = "Fighter", HitDieSize = 10,  AverageHitDieRoll = 6, ClassAbilityScore = "Strength", CasterType = casterTypes.Single(x => x.Id == CasterTypes.NonCaster) },
            new() { Id = Classes.Monk, Name = "Monk", HitDieSize = 8,  AverageHitDieRoll = 5, ClassAbilityScore = "Wisdom", CasterType = casterTypes.Single(x => x.Id == CasterTypes.NonCaster) },
            new() { Id = Classes.Paladin, Name = "Paladin", HitDieSize = 10,  AverageHitDieRoll = 6, ClassAbilityScore = "Charisma", CasterType = casterTypes.Single(x => x.Id == CasterTypes.HalfCaster) },
            new() { Id = Classes.Ranger, Name = "Ranger", HitDieSize = 10,  AverageHitDieRoll = 6, ClassAbilityScore = "Wisdom", CasterType = casterTypes.Single(x => x.Id == CasterTypes.HalfCaster) },
            new() { Id = Classes.Rogue, Name = "Rogue", HitDieSize = 8,  AverageHitDieRoll = 5, ClassAbilityScore = "Dexterity", CasterType = casterTypes.Single(x => x.Id == CasterTypes.NonCaster) },
            new() { Id = Classes.Sorcerer, Name = "Sorcerer", HitDieSize = 6,  AverageHitDieRoll = 4, ClassAbilityScore = "Charisma", CasterType = casterTypes.Single(x => x.Id == CasterTypes.FullCaster) },
            new() { Id = Classes.Warlock, Name = "Warlock", HitDieSize = 8,  AverageHitDieRoll = 5, ClassAbilityScore = "Charisma", CasterType = casterTypes.Single(x => x.Id == CasterTypes.Warlock) },
            new() { Id = Classes.Wizard, Name = "Wizard", HitDieSize = 6,  AverageHitDieRoll = 4, ClassAbilityScore = "Intelligence", CasterType = casterTypes.Single(x => x.Id == CasterTypes.FullCaster) }

        };

        Seed(classSeeds);
    }

    private void SeedPrimalCompanionTypes()
    {
        var primalCompanionTypeSeeds = new List<PrimalCompanionType>
        {
            new()
            {
                Id = 1,
                Name = "Beast of the Land",
                HitPointMultiplier = 5,
                HitDieSize = 8,
                Speed = "40 ft., climb 40 ft.",
                Size = "Medium",
                Strength = 14,
                Dexterity = 14,
                Constitution = 15,
                Intelligence = 8,
                Wisdom = 14,
                Charisma = 11,
                AbilityName = "Charge",
                AbilityDescription = "If the beast moves at least 20 feet straight toward a target and then hits it with a maul attack on the same turn, the target takes an extra 1d6 slashing damage. If the target is a creature, it must succeed on a <wisSpellSave> DC Strength or be knocked prone.",
                ActionName = "Maul",
                ActionDescription = "<em>Melee Weapon Attack:</em> +<toHitBonus> to hit, reach 5 ft., one target.<br/><em>Hit:</em> 1d8 + <strDamage> slashing damage."
            },
            new()
            {
                Id = 2,
                Name = "Beast of the Sea",
                HitPointMultiplier = 5,
                HitDieSize = 8,
                Speed = "5 ft., swim 60 ft.",
                Size = "Medium",
                Strength = 14,
                Dexterity = 14,
                Constitution = 15,
                Intelligence = 8,
                Wisdom = 14,
                Charisma = 11,
                AbilityName = "Amphibious",
                AbilityDescription = "The beast can breathe both air and water.",
                ActionName = "Binding Strike",
                ActionDescription = "<em>Melee Weapon Attack:</em> +<toHitBonus> to hit, reach 5 ft., one target.<br/><em>Hit:</em> 1d6 + <strDamage> piercing or bludgeoning damage (your choice), and the target is grappled (escape DC <wisSpellSave>). Until this grapple ends, the beast can't use this attack on another target."
            },
            new()
            {
                Id = 3,
                Name = "Beast of the Sky",
                HitPointMultiplier = 4,
                HitDieSize = 6,
                Speed = "10 ft., fly 60 ft.",
                Size = "Small",
                Strength = 6,
                Dexterity = 16,
                Constitution = 13,
                Intelligence = 8,
                Wisdom = 14,
                Charisma = 11,
                AbilityName = "Flyby",
                AbilityDescription = "The beast doesn't provoke opportunity attacks when it flies out of an enemy's reach.",
                ActionName = "Shred",
                ActionDescription = "<em>Melee Weapon Attack:</em> +<toHitBonus> to hit, reach 5 ft., one target.<br/><em>Hit:</em> 1d4 + <dexDamage> slashing damage."
            }
        };

        Seed(primalCompanionTypeSeeds);
    }

    public void SeedStressTypes()
    {
        var stressTypeSeeds = new List<StressType>
        {
            new() { Id = StressTypes.Affliction, Name = "Affliction", MinimumRoll = 1, MaximumRoll = 9 },
            new() { Id = StressTypes.Virtue, Name = "Virtue", MinimumRoll = 10, MaximumRoll = 10 }
        };

        Seed(stressTypeSeeds);
    }

    public void SeedStressStatuses()
    {
        var affliction = dbContext.Find<StressType>(StressTypes.Affliction)!;
        var virtue = dbContext.Find<StressType>(StressTypes.Virtue)!;

        var statusSeeds = new List<StressStatus>
        {
            new()
            {
                Id = 1,
                Name = "Irrational",
                Description = "On each of your turns, before taking any other actions, you must use your movement to move as close as you can in a straight line towards a random creature that you can see. You cannot select the same creature two turns in a row. You have disadvantage on all Intelligence checks and saving throws.",
                Roll = 1,
                StressType = affliction
            },
            new()
            {
                Id = 2,
                Name = "Paranoid",
                Description = "You cannot be the target of, or gain any benefits of, your allies' spells, actions, abilities, or items. You cannot willingly move within 5 feet of an ally. If an ally is within 5 feet of you at any point during your turn, you must use your movement to get away from that creature, moving in a random direction, before taking any other actions. You have disadvantage on all Dexterity checks and saving throws.",
                Roll = 2,
                StressType = affliction
            },
            new()
            {
                Id = 3,
                Name = "Selfish",
                Description = "You cannot cast spells targeting allies other than spells that cause damage, you may not use the help action, you cannot give items to your allies, and you cannot use non-damaging items on your allies. You have disadvantage on all Charisma checks and saving throws.",
                Roll = 3,
                StressType = affliction
            },
            new()
            {
                Id = 4,
                Name = "Abusive",
                Description = "At the start of your turn, roll 1d6. On a 5 or higher, all allies within 50 feet of you that can hear you gain 1d4 stress points. On a 6, they also disadvantage on their next attack roll, ability check, or saving throw. You have disadvantage on all Constitution checks and saving throws.",
                Roll = 4,
                StressType = affliction
            },
            new()
            {
                Id = 5,
                Name = "Fearful",
                Description = "At the start of your turn, randomly select one hostile creature that you can see that you are not afraid of. Until the end of your turn, you are afraid of that creature (this does not trigger the stress gained due to gaining the Fear condition). You have disadvantage on all Wisdoms skill checks and saving throws.",
                Roll = 5,
                StressType = affliction
            },
            new()
            {
                Id = 6,
                Name = "Hopeless",
                Description = "You cannot have or gain advantage on any rolls. When you gain stress points, you gain an additional 2 stress. When you lose stress points, reduce the amount lost by 2 (to a minimum of 1). You have disadvantage on all Resolve saving throws.",
                Roll = 6,
                StressType = affliction
            },
            new()
            {
                Id = 7,
                Name = "Masochistic",
                Description = "If you are engaged with a hostile creature, you cannot take any actions or use any movement which would cause you to become disengaged from that creature. Attacks made against you may reroll one damage die. You have disadvantage on all Strength checks and saving throws.",
                Roll = 7,
                StressType = affliction
            },
            new()
            {
                Id = 8,
                Name = "Powerful",
                Description = "Add your your Resolve modifier to all your damage rolls.",
                Roll = 1,
                StressType = virtue
            },
            new()
            {
                Id = 9,
                Name = "Courageous",
                Description = "Whenever you gain stress, reduce the amount of stress gained by your Resolve modifier (to a minimum of 1).",
                Roll = 2,
                StressType = virtue
            },
            new()
            {
                Id = 10,
                Name = "Stalwart",
                Description = "All damage dealt to you is reduced by your Resolve modified (to a minimum of 1).",
                Roll = 3,
                StressType = virtue
            },
            new()
            {
                Id = 11,
                Name = "Vigorous",
                Description = "Add your Resolve modifier to your AC and to all saving throws.",
                Roll = 4,
                StressType = virtue
            },
            new()
            {
                Id = 12,
                Name = "Focused",
                Description = "Add your Resolve modifier to all your attack rolls and your Spell Save DC.",
                Roll = 5,
                StressType = virtue
            }
        };

        Seed(statusSeeds);
    }

    private void SeedChallengeRatings()
    {
        var crSeeds = new List<ChallengeRating>
        {
            new() { Id = 1, CR = "0", XP = 0, ProficiencyBonus = 2 },
            new() { Id = 2, CR = "0", XP = 10, ProficiencyBonus = 2 },
            new() { Id = 3, CR = "1/8", XP = 25, ProficiencyBonus = 2 },
            new() { Id = 4, CR = "1/4", XP = 50, ProficiencyBonus = 2 },
            new() { Id = 5, CR = "1/2", XP = 100, ProficiencyBonus = 2 },
            new() { Id = 6, CR = "1", XP = 200, ProficiencyBonus = 2 },
            new() { Id = 7, CR = "2", XP = 450, ProficiencyBonus = 2 },
            new() { Id = 8, CR = "3", XP = 700, ProficiencyBonus = 2 },
            new() { Id = 9, CR = "4", XP = 1100, ProficiencyBonus = 2 },
            new() { Id = 10, CR = "5", XP = 1800, ProficiencyBonus = 3 },
            new() { Id = 11, CR = "6", XP = 2300, ProficiencyBonus = 3 },
            new() { Id = 12, CR = "7", XP = 2900, ProficiencyBonus = 3 },
            new() { Id = 13, CR = "8", XP = 3900, ProficiencyBonus = 3 },
            new() { Id = 14, CR = "9", XP = 5000, ProficiencyBonus = 4 },
            new() { Id = 15, CR = "10", XP = 5900, ProficiencyBonus = 4 },
            new() { Id = 16, CR = "11", XP = 7200, ProficiencyBonus = 4 },
            new() { Id = 17, CR = "12", XP = 8400, ProficiencyBonus = 4 },
            new() { Id = 18, CR = "13", XP = 10000, ProficiencyBonus = 5 },
            new() { Id = 19, CR = "14", XP = 11500, ProficiencyBonus = 5 },
            new() { Id = 20, CR = "15", XP = 13000, ProficiencyBonus = 5 },
            new() { Id = 21, CR = "16", XP = 15000, ProficiencyBonus = 5 },
            new() { Id = 22, CR = "17", XP = 18000, ProficiencyBonus = 6 },
            new() { Id = 23, CR = "18", XP = 20000, ProficiencyBonus = 6 },
            new() { Id = 24, CR = "19", XP = 22000, ProficiencyBonus = 6 },
            new() { Id = 25, CR = "20", XP = 25000, ProficiencyBonus = 6 },
            new() { Id = 26, CR = "21", XP = 33000, ProficiencyBonus = 7 },
            new() { Id = 27, CR = "22", XP = 41000, ProficiencyBonus = 7 },
            new() { Id = 28, CR = "23", XP = 50000, ProficiencyBonus = 7 },
            new() { Id = 29, CR = "24", XP = 62000, ProficiencyBonus = 7 },
            new() { Id = 30, CR = "25", XP = 75000, ProficiencyBonus = 8 },
            new() { Id = 31, CR = "26", XP = 90000, ProficiencyBonus = 8 },
            new() { Id = 32, CR = "27", XP = 105000, ProficiencyBonus = 8 },
            new() { Id = 33, CR = "28", XP = 120000, ProficiencyBonus = 8 },
            new() { Id = 34, CR = "29", XP = 135500, ProficiencyBonus = 9 },
            new() { Id = 35, CR = "30", XP = 155000, ProficiencyBonus = 9 }
        };

        Seed(crSeeds);
    }

    private void SeedXpThresholds()
    {
        var xpThreholdSeeds = new List<EncounterXpThreshold>
        {
            new() { Id = 1, Easy = 25, Medium = 50, Hard = 75, Deadly = 100, Budget = 300 },
            new() { Id = 2, Easy = 50, Medium = 100, Hard = 150, Deadly = 200, Budget = 600 },
            new() { Id = 3, Easy = 75, Medium = 150, Hard = 225, Deadly = 400,  Budget =1200 },
            new() { Id = 4, Easy = 125, Medium = 250, Hard = 375, Deadly = 500, Budget = 1700 },
            new() { Id = 5, Easy = 250, Medium = 500, Hard = 750, Deadly = 1100, Budget = 3500 },
            new() { Id = 6, Easy = 300, Medium = 600, Hard = 900, Deadly = 1400, Budget = 4000 },
            new() { Id = 7, Easy = 350, Medium = 750, Hard = 1100, Deadly = 1700, Budget = 5000 },
            new() { Id = 8, Easy = 450, Medium = 900, Hard = 1400, Deadly = 2100, Budget = 6000 },
            new() { Id = 9, Easy = 550, Medium = 1100, Hard = 1600, Deadly = 2400, Budget = 7500 },
            new() { Id = 10, Easy = 600, Medium = 1200, Hard = 1900, Deadly = 2800, Budget = 9000 },
            new() { Id = 11, Easy = 800, Medium = 1600, Hard = 2400, Deadly = 3600, Budget = 10500 },
            new() { Id = 12, Easy = 1000, Medium = 2000, Hard = 3000, Deadly = 4500, Budget = 11500 },
            new() { Id = 13, Easy = 1100, Medium = 2200, Hard = 3400, Deadly = 5100, Budget = 13500 },
            new() { Id = 14, Easy = 1250, Medium = 2500, Hard = 3800, Deadly = 5700, Budget = 15000 },
            new() { Id = 15, Easy = 1400, Medium = 2800, Hard = 4300, Deadly = 6400, Budget = 18000 },
            new() { Id = 16, Easy = 1600, Medium = 3200, Hard = 4800, Deadly = 7200, Budget = 20000 },
            new() { Id = 17, Easy = 2000, Medium = 3900, Hard = 5900, Deadly = 8800, Budget = 25000 },
            new() { Id = 18, Easy = 2100, Medium = 4200, Hard = 6300, Deadly = 9500, Budget = 27000 },
            new() { Id = 19, Easy = 2400, Medium = 4900, Hard = 7300, Deadly = 10900, Budget = 30000 },
            new() { Id = 20, Easy = 2800, Medium = 5700, Hard = 8500, Deadly = 12700, Budget = 40000 }
        };

        Seed(xpThreholdSeeds);
    }

    private void SeedItems()
    {
        var itemSeeds = new List<Item>
        {
            new() { Id = 1, Name = "Potion of Healing" },
            new() { Id = 2, Name = "Potion of Healing (Greater)" },
            new() { Id = 3, Name = "Potion of Healing (Superior)" },
            new() { Id = 4, Name = "Potion of Healing (Supreme)" },
            new() { Id = 5, Name = "Torch" },
            new() { Id = 6, Name = "Rope, Hempen (50 feet)" },
            new() { Id = 7, Name = "Tent" },
            new() { Id = 8, Name = "Crowbar" },
            new() { Id = 9, Name = "Hammer" },
            new() { Id = 10, Name = "Piton" },
            new() { Id = 11, Name = "Hooded Lantern" },
            new() { Id = 12, Name = "Tinderbox" },
            new() { Id = 13, Name = "Waterskin" },
            new() { Id = 14, Name = "Sheet of Paper" },
            new() { Id = 15, Name = "Sealing Wax" },
            new() { Id = 16, Name = "Ball Bearings (x1000)" },
            new() { Id = 17, Name = "Flask (Oil)" },
            new() { Id = 18, Name = "Vial of Perfume" },
            new() { Id = 19, Name = "Soap" },
            new() { Id = 20, Name = "Common Clothes" },
            new() { Id = 21, Name = "Fine Clothes" },
            new() { Id = 22, Name = "Bottle of Ink" },
            new() { Id = 23, Name = "Ink Pen" },
            new() { Id = 24, Name = "Lamp" },
            new() { Id = 25, Name = "Costume" },
            new() { Id = 26, Name = "Candle" },
            new() { Id = 27, Name = "Disguise Kit" },
            new() { Id = 28, Name = "Bedroll" },
            new() { Id = 29, Name = "Mess Kit" },
            new() { Id = 30, Name = "Blanket" },
            new() { Id = 31, Name = "Alms Box" },
            new() { Id = 32, Name = "Block of Incense" },
            new() { Id = 33, Name = "Censer" },
            new() { Id = 34, Name = "Vestment" },
            new() { Id = 35, Name = "Sheet of Parchment" },
            new() { Id = 36, Name = "Bag of Sand" },
            new() { Id = 37, Name = "Small Knife" },
            new() { Id = 38, Name = "Forgery Kit" },
            new() { Id = 39, Name = "Herbalism Kit" },
            new() { Id = 40, Name = "Navigator's Tools" },
            new() { Id = 41, Name = "Provisioner's Kit" },
            new() { Id = 42, Name = "Thieves' Tools" }
        };

        Seed(itemSeeds);
    }

    private void Seed<TEntity>(IEnumerable<TEntity> seeds) where TEntity : class
    {
        var existingEntities = dbContext.Set<TEntity>().ToList();

        using var transaction = dbContext.Database.BeginTransaction();

        foreach (var seed in seeds)
        {
            var existingEntity = existingEntities
                .SingleOrDefault(x => (int) typeof(TEntity).GetProperty("Id")!.GetValue(x)! == (int) typeof(TEntity).GetProperty("Id")!.GetValue(seed)!);

            if (existingEntity == default)
            {
                dbContext.Add(seed);
            }
            else
            {
                dbContext.Entry(existingEntity).CurrentValues.SetValues(seed);
            }
        }

        dbContext.SaveChanges();

        transaction.Commit();
    }
}
