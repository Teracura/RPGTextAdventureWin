using Entities.Heroes;

namespace MainLogic.GameLogic.CombatLogic.SpecialAttacks;

public class AbsoluteGuard() : ISpecialAttack
{
    public string Name { get; } = "Aboslute Guard";
    public int ManaCost { get; } = 20;
    public int turnCooldown { get; } = 0;
    public HeroClasses HeroClass { get; } = HeroClasses.Warrior;
    public int RealCooldown { get; set; }
    
    public void Apply(IHero hero)
    {
        throw new NotImplementedException();
    }

    public string GetDescription()
    {
        return "All attacks deal 80% damage for the next 3 turns and increases defense by 30%";
    }
}