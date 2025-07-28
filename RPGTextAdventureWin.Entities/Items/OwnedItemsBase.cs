using System.ComponentModel.DataAnnotations;

namespace Entities.Items;

public class OwnedItemsBase
{
    [Key]
    public int SaveSlot { get; set; }
    // Common Items count
    public int Common_SmallHealingPotion { get; set; }
    public int Common_BigHealingPotion { get; set; }
    public int Common_BlessingOfLife { get; set; }
    public int Common_SmallManaPotion { get; set; }
    public int Common_BigManaPotion { get; set; }
    public int Common_BlessingOfTheMagician { get; set; }
    public int Common_KaitoKey { get; set; }

    // Warrior Items count
    public int Warrior_WoodenSword { get; set; }
    public int Warrior_IronSword { get; set; }
    public int Warrior_SwordOfMarcus { get; set; }
    public int Warrior_LeatherArmor { get; set; }
    public int Warrior_ChainMailArmor { get; set; }
    public int Warrior_PlateArmor { get; set; }
    public int Warrior_ArmorOfMarcus { get; set; }
    public int Warrior_WoodenShield { get; set; }
    public int Warrior_IronShield { get; set; }
    public int Warrior_ShieldOfMarcus { get; set; }

    // Archer Items count
    public int Archer_QuickBow { get; set; }
    public int Archer_LongBow { get; set; }
    public int Archer_Crossbow { get; set; }
    public int Archer_BowOfTheElves { get; set; }
    public int Archer_TraineeArmor { get; set; }
    public int Archer_HunterArmor { get; set; }
    public int Archer_ArmorOfTheElves { get; set; }
    public int Archer_SneakingBoots { get; set; }
    public int Archer_BootsOfTheElves { get; set; }

    // Mage Items count
    public int Mage_WoodenStaff { get; set; }
    public int Mage_WizardStaff { get; set; }
    public int Mage_StaffOfTheRoyalWizard { get; set; }
    public int Mage_StaffOfTheArchmage { get; set; }
    public int Mage_ManaAmulet { get; set; }
    public int Mage_SpellBook { get; set; }
    public int Mage_OrbOfTheArchmage { get; set; }
    public int Mage_CharmOfTheWind { get; set; }
}
