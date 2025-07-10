using Entities.Heroes;

namespace MainLogic.GameLogic.CombatLogic.SpecialAttacks.MageSpecialAttacks;

public class SoulSwap : ISpecialAttack
{
    public string Name { get; } = "Soul Swap";
    public int ManaCost { get; } = 0;
    public int turnCooldown { get; } = 4;
    public HeroClasses HeroClass { get; } = HeroClasses.Mage;
    public void Apply(IHero hero)
    {
        throw new NotImplementedException();
    }

    public string GetDescription()
    {
        return "Swaps Mana and Hp percentages, use at your own risk";
    }
}