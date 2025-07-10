using Entities.Heroes;

namespace MainLogic.GameLogic.CombatLogic.SpecialAttacks.WarriorSpecialAttacks;

public class StrikeOfTheAvenger : ISpecialAttack
{
    public string Name { get; } = "Strike of the Avenger";
    public int ManaCost { get; } = 15;
    public int turnCooldown { get; } = 0;
    public HeroClasses HeroClass { get; } = HeroClasses.Warrior;
    public void Apply(IHero hero)
    {
        var enemy = CombatManager.CurrentEnemy;
        enemy.Hp -= hero.Dmg *
                    (Math.Max(1, 4 * (hero.CalculateMaxHp() - (hero.Hp + 1) / hero.CalculateMaxHp())));
    }

    public string GetDescription()
    {
        return "deals higher damage the lower the HP is (caps at 400%)";
    }
}