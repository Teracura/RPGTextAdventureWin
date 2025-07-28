namespace Entities.Items;

public enum ItemTypes
{
    // Common items (prefix with Common_)
    Common_SmallHealingPotion,
    Common_BigHealingPotion,
    Common_BlessingOfLife,
    Common_SmallManaPotion,
    Common_BigManaPotion,
    Common_BlessingOfTheMagician,
    Common_KaitoKey,
    
    // Warrior specific items (prefix with Warrior_)
    Warrior_WoodenSword,
    Warrior_IronSword,
    Warrior_SwordOfMarcus, //don't ask who is marcus
    Warrior_LeatherArmor,
    Warrior_ChainMailArmor,
    Warrior_PlateArmor,
    Warrior_ArmorOfMarcus,
    Warrior_WoodenShield,
    Warrior_IronShield,
    Warrior_ShieldOfMarcus,
    
    // Archer specific items (prefix with Archer_)
    Archer_QuickBow,
    Archer_LongBow,
    Archer_Crossbow,
    Archer_BowOfTheElves,
    Archer_TraineeArmor,
    Archer_HunterArmor,
    Archer_ArmorOfTheElves,
    Archer_SneakingBoots,
    Archer_BootsOfTheElves,
    
    // Mage specific items (prefix with Mage_)
    Mage_WoodenStaff,
    Mage_WizardStaff,
    Mage_StaffOfTheRoyalWizard,
    Mage_StaffOfTheArchmage,
    Mage_ManaAmulet,
    Mage_SpellBook,
    Mage_OrbOfTheArchmage,
    Mage_CharmOfTheWind,
    
    // Literally nothing
    Nothing,
}