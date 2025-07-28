using Entities.Heroes;

namespace MainLogic.GameLogic.CombatLogic.SpecialAttacks.ArcherSpecialAttacks;

public class TheFierceShot : ISpecialAttack
{
    public string Name { get; } = "The Fierce Shot";
    public int ManaCost { get; } = 10;
    public int turnCooldown { get; } = 1;
    public HeroClasses HeroClass { get; } = HeroClasses.Archer;
    public int RealCooldown { get; set; }
    
    public void Apply(IHero hero)
    {
        throw new NotImplementedException();
    }

    public string GetDescription()
    {
        return "deals more damage the higher the HP is (caps at 300% and min is 100%)";
    }
}