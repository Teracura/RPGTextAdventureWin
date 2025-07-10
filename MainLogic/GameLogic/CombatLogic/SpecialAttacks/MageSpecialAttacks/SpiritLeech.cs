using Entities.Heroes;

namespace MainLogic.GameLogic.CombatLogic.SpecialAttacks.MageSpecialAttacks;

public class SpiritLeech : ISpecialAttack
{
    public string Name { get; } = "Spirit leech";
    public int ManaCost { get; } = 0;
    public int turnCooldown { get; } = 1;
    public HeroClasses HeroClass { get; } = HeroClasses.Mage;
    public void Apply(IHero hero)
    {
        throw new NotImplementedException();
    }

    public string GetDescription()
    {
        return "next attack deals 50% less damage and mana is increased by 10-15% (randomly)";
    }
}