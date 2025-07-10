using Entities.Heroes;

namespace MainLogic.GameLogic.CombatLogic.SpecialAttacks.ArcherSpecialAttacks;

public class FocusShot : ISpecialAttack
{
    public string Name { get; } = "Focus Shot";
    public int ManaCost { get; } = 15;
    public int turnCooldown { get; } = 2;
    public HeroClasses HeroClass { get; } = HeroClasses.Archer;
    public void Apply(IHero hero)
    {
        throw new NotImplementedException();
    }

    public string GetDescription()
    {
        return "deals 300% damage but takes 25% more damage for 2 turns";
    }
}