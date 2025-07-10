using MainLogic.GameLogic;
using MainLogic.GameLogic.CombatLogic.SpecialAttacks;
using MainLogic.GameLogic.CombatLogic.SpecialAttacks.ArcherSpecialAttacks;
using MainLogic.GameLogic.CombatLogic.SpecialAttacks.MageSpecialAttacks;
using MainLogic.GameLogic.CombatLogic.SpecialAttacks.WarriorSpecialAttacks;

namespace MainLogic.Factories;

public static class SpecialAttackFactory
{
    public static ISpecialAttack Create(SpecialAttacks type)
    {
        return type switch
        {
            // Warrior
            SpecialAttacks.StrikeOfTheAvenger => new StrikeOfTheAvenger(),
            SpecialAttacks.AbsoluteGuard => new AbsoluteGuard(),
            SpecialAttacks.UltimateRageStrike => new UltimateRageStrike(),

            // Mage
            SpecialAttacks.SoulSwap => new SoulSwap(),
            SpecialAttacks.SparkOfTheMightiestMage => new SparkOfTheMightiestMage(),
            SpecialAttacks.SpiritLeech => new SpiritLeech(),

            // Archer
            SpecialAttacks.TheFierceShot => new TheFierceShot(),
            SpecialAttacks.ElvenCursedArrow => new ElvenCursedArrow(),
            SpecialAttacks.FocusShot => new FocusShot(),

            _ => throw new Exception("Listed special attack is not in factory")
        };
    }
}