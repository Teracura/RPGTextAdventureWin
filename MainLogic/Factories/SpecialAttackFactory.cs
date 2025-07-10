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
}