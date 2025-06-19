using Entities.Heroes;

namespace Entities.Items.ItemEffects;

public interface IItemEffect
{
    bool Apply(IHero hero);
    string GetDescription();
}