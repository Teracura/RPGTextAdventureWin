using Entities.Heroes;

namespace MainLogic.GameLogic;

public interface ISpecialAttack
{
    public string Name { get; }
    public int ManaCost { get; }
    public int turnCooldown { get; }
    public HeroClasses HeroClass { get; }
    public void Apply(IHero hero);
    public string GetDescription();
}