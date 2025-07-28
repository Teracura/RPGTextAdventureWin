using Entities.Heroes;
using Entities.Items;

namespace MainLogic.GlobalParameters;

public class HeroState
{
    public IHero Hero { get; set; } = null!;
    public bool IsDefeated { get; set; }
    public ItemTypes EquippedWeapon { get; set; } = ItemTypes.Nothing;
    public ItemTypes EquippedArmor { get; set; } = ItemTypes.Nothing;
    public ItemTypes EquippedAccessory { get; set; } = ItemTypes.Nothing;
}