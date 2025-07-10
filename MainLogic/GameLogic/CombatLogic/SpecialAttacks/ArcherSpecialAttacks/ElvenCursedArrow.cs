using Entities.Heroes;

namespace MainLogic.GameLogic.CombatLogic.SpecialAttacks.ArcherSpecialAttacks;

public class ElvenCursedArrow : ISpecialAttack
{
    public string Name { get; } = "Elven Cursed Arrow";
    public int ManaCost { get; } = 10;
    public int turnCooldown { get; } = 0;
    public HeroClasses HeroClass { get; } = HeroClasses.Archer;
    public void Apply(IHero hero)
    {
        throw new NotImplementedException();
    }

    public string GetDescription()
    {
        return "steals enemy HP equal to damage dealt";
    }
}