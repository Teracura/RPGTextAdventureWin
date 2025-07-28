using Entities.Heroes;
using MainLogic.GameLogic;
using MainLogic.GameLogic.CombatLogic.SpecialAttacks;
using MainLogic.GameLogic.CombatLogic.SpecialAttacks.ArcherSpecialAttacks;
using MainLogic.GameLogic.CombatLogic.SpecialAttacks.MageSpecialAttacks;
using MainLogic.GameLogic.CombatLogic.SpecialAttacks.WarriorSpecialAttacks;

namespace MainLogic.Factories;

public static class SpecialAttackFactory
{
    public static ISpecialAttack Create(SpecialAttackTypes type)
    {
        return type switch
        {
            // Warrior
            SpecialAttackTypes.StrikeOfTheAvenger => new StrikeOfTheAvenger(),
            SpecialAttackTypes.AbsoluteGuard => new AbsoluteGuard(),
            SpecialAttackTypes.UltimateRageStrike => new UltimateRageStrike(),

            // Mage
            SpecialAttackTypes.SoulSwap => new SoulSwap(),
            SpecialAttackTypes.SparkOfTheMightiestMage => new SparkOfTheMightiestMage(),
            SpecialAttackTypes.SpiritLeech => new SpiritLeech(),

            // Archer
            SpecialAttackTypes.TheFierceShot => new TheFierceShot(),
            SpecialAttackTypes.ElvenCursedArrow => new ElvenCursedArrow(),
            SpecialAttackTypes.FocusShot => new FocusShot(),

            _ => throw new Exception("Listed special attack is not in factory")
        };
    }

    public static List<ISpecialAttack> CreateAllSameClass(HeroClasses heroClass)
    {
        var result = new List<ISpecialAttack>();
        switch (heroClass)
        {
            case HeroClasses.Mage:
                result.Add(Create(SpecialAttackTypes.SoulSwap));
                result.Add(Create(SpecialAttackTypes.SparkOfTheMightiestMage));
                result.Add(Create(SpecialAttackTypes.SpiritLeech));
                return result;
            case HeroClasses.Warrior:
                result.Add(Create(SpecialAttackTypes.StrikeOfTheAvenger));
                result.Add(Create(SpecialAttackTypes.AbsoluteGuard));
                result.Add(Create(SpecialAttackTypes.UltimateRageStrike));
                return result;
            case HeroClasses.Archer:
                result.Add(Create(SpecialAttackTypes.TheFierceShot));
                result.Add(Create(SpecialAttackTypes.ElvenCursedArrow));
                result.Add(Create(SpecialAttackTypes.FocusShot));
                return result;
            default:
                throw new Exception($"Unknown Hero Type: {heroClass}");
        }
    }
    
    public static List<ISpecialAttack> CreateAllSameClass(string heroClass)
    {
        var result = new List<ISpecialAttack>();
        heroClass = heroClass.ToLower();
        switch (heroClass)
        {
            case "mage":
                result.Add(Create(SpecialAttackTypes.SoulSwap));
                result.Add(Create(SpecialAttackTypes.SparkOfTheMightiestMage));
                result.Add(Create(SpecialAttackTypes.SpiritLeech));
                return result;
            case "warrior":
                result.Add(Create(SpecialAttackTypes.StrikeOfTheAvenger));
                result.Add(Create(SpecialAttackTypes.AbsoluteGuard));
                result.Add(Create(SpecialAttackTypes.UltimateRageStrike));
                return result;
            case "archer":
                result.Add(Create(SpecialAttackTypes.TheFierceShot));
                result.Add(Create(SpecialAttackTypes.ElvenCursedArrow));
                result.Add(Create(SpecialAttackTypes.FocusShot));
                return result;
            default:
                throw new Exception($"Unknown Hero Type: {heroClass}");
        }
    }
}