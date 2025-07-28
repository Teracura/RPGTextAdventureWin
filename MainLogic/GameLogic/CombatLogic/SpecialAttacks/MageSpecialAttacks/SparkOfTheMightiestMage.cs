using Entities.Heroes;
using MainLogic.GlobalParameters;

namespace MainLogic.GameLogic.CombatLogic.SpecialAttacks.MageSpecialAttacks;

public class SparkOfTheMightiestMage : ISpecialAttack
{
    public string Name { get; } = "Spark of the Mightiest Mage";
    public int ManaCost { get; set; } = -1; //will have a dynamic value upon using
    public int turnCooldown { get; } = 0;
    public HeroClasses HeroClass { get; } = HeroClasses.Mage;
    public int RealCooldown { get; set; }
    
    public void Apply(IHero hero)
    {
        var attackBoost = hero.Mp * 0.05m;
        hero.Mp = 0;
    }

    public string GetDescription()
    {
        return "does 0% attack damage, but consumes all mana with every 1 mana increasing attack by 5%";
    }
}