using Entities.Heroes;

namespace MainLogic.GameLogic.CombatLogic.SpecialAttacks.WarriorSpecialAttacks;

public class UltimateRageStrike : ISpecialAttack
{
    public string Name { get; } = "Ultimate Rage Strike";
    public int ManaCost { get; } = 5;
    public int turnCooldown { get; } = 0;
    public HeroClasses HeroClass { get; } = HeroClasses.Warrior;
    public void Apply(IHero hero)
    {
        CombatManager.CurrentEnemy.Hp -= hero.Dmg * 1.5m;
    }

    public string GetDescription()
    {
        return "deals 150% of attack damage";
    }
}