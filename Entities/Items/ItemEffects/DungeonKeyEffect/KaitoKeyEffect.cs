using Entities.Heroes;

namespace Entities.Items.ItemEffects.DungeonKeyEffect;

public class KaitoKeyEffect : IItemEffect
{
    public bool Apply(IHero hero)
    {
        //TODO: GameStateParameters.KaitoKeyEffect = true;.... figure it out bozo
        return true;
    }

    public string GetDescription()
    {
        return "Key to opening horrors beyond comprehension"; //TODO: change this cringe description
    }
}