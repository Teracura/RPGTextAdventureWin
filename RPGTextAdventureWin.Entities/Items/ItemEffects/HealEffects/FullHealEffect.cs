using Entities.Heroes;

namespace Entities.Items.ItemEffects.HealEffects;

public class FullHealEffect : IItemEffect
{
    public bool Apply(IHero hero)
    {
        hero.Hp = hero.CalculateMaxHp();
        return true;
    }

    public string GetDescription()
    {
        return $"Restores the maximum amount of HP";
    }
}