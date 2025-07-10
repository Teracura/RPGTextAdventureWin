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
        var manaRatio = (hero.CalculateMaxMp() - hero.Mp / hero.CalculateMaxMp());
        var hpRatio = (hero.CalculateMaxHp() - hero.Hp / hero.CalculateMaxHp());
        hero.Hp = hero.CalculateMaxHp() * manaRatio;
        hero.Mp = hero.CalculateMaxMp() * hpRatio;
    }

    public string GetDescription()
    {
        return "Swaps Mana and Hp percentages, use at your own risk";
    }
}