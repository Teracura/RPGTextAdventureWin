using Entities.Heroes;

namespace Entities.Items.ItemEffects.ManaEffects;

public class FullManaGainEffect : IItemEffect
{
    public bool Apply(IHero hero)
    {
        hero.Mp = hero.CalculateMaxMp();
        return true;
    }

    public string GetDescription()
    {
        return $"Restores the maximum amount of MP";
    }
}