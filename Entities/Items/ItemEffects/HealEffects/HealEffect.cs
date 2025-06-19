using Entities.Heroes;

namespace Entities.Items.ItemEffects.HealEffects;

public class HealEffect(int amount) : IItemEffect
{
    public bool Apply(IHero hero)
    {
        hero.Hp += amount;
        if (hero.Hp > hero.CalculateMaxHp()) hero.Hp = hero.CalculateMaxHp();
        return true;
    }

    public string GetDescription()
    {
        return $"Restores {amount} HP";
    }
}