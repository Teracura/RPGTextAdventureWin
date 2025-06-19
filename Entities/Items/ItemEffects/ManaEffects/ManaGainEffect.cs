using Entities.Heroes;

namespace Entities.Items.ItemEffects.ManaEffects;

public class ManaGainEffect(int amount) : IItemEffect
{
    public bool Apply(IHero hero)
    {
        hero.Mp += amount;
        if (hero.Mp > hero.CalculateMaxHp()) hero.Mp = hero.CalculateMaxHp();
        return true;
    }

    public string GetDescription()
    {
        return $"Restores {amount} MP";
    }
}