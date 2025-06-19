using Entities.Items;
using Entities.Items.ItemEffects;
using Entities.Items.ItemEffects.DungeonKeyEffect;
using Entities.Items.ItemEffects.HealEffects;
using Entities.Items.ItemEffects.ManaEffects;

namespace MainLogic.Factories;

public static class ItemEffectFactory
{
    public static IItemEffect? Create(ItemTypes type)
    {
        return type switch
        {
            ItemTypes.Common_SmallHealingPotion => new HealEffect(50),
            ItemTypes.Common_BigHealingPotion => new HealEffect(150),
            ItemTypes.Common_BlessingOfLife => new FullHealEffect(),
            
            ItemTypes.Common_SmallManaPotion => new ManaGainEffect(40),
            ItemTypes.Common_BigManaPotion => new ManaGainEffect(120),
            ItemTypes.Common_BlessingOfTheMagician => new FullManaGainEffect(),
            
            ItemTypes.Common_KaitoKey => new KaitoKeyEffect(),

            // Default case: non-usable items like weapons/armors
            _ => null
        };
    }
}