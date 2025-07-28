using Entities.Heroes;
using MainLogic.GameLogic.CombatLogic.SpecialAttacks;

namespace MainLogic.GameLogic;

public interface ISpecialAttack
{
    public string Name { get; }
    public int ManaCost { get; }
    public int turnCooldown { get; }
    public HeroClasses HeroClass { get; }
    public int RealCooldown { get; set; }
    public void Apply(IHero hero);

    public SpecialAttackTypes GetType()
    {
        var name = Name.ToLower();
        switch (name)
        {
            // Warrior
            case "strikeoftheavenger":
                return SpecialAttackTypes.StrikeOfTheAvenger;
            case "absoluteguard":
                return SpecialAttackTypes.AbsoluteGuard;
            case "ultimateragestrike":
                return SpecialAttackTypes.UltimateRageStrike;

            // Mage
            case "soulswap":
                return SpecialAttackTypes.SoulSwap;
            case "sparkofthemightiestmage":
                return SpecialAttackTypes.SparkOfTheMightiestMage;
            case "spiritleech":
                return SpecialAttackTypes.SpiritLeech;

            // Archer
            case "thefierceshot":
                return SpecialAttackTypes.TheFierceShot;
            case "elvencursedarrow":
                return SpecialAttackTypes.ElvenCursedArrow;
            case "focusshot":
                return SpecialAttackTypes.FocusShot;

            default:
                throw new ArgumentException($"Unknown special attack name: {Name}");
        }
    }

    public string GetDescription();
}