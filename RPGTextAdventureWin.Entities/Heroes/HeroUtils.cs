using Entities.Enemies;
using Entities.Items;

namespace Entities.Heroes;

public static class HeroUtils
{
    public static decimal CalculateXpNeededForLevel(int level)
    {
        return level switch
        {
            <= 20 => (int)Math.Floor(Math.Pow(level, 1.5) * 100),
            <= 40 => (int)Math.Floor(Math.Pow(level, 1.47) * 115),
            <= 60 => (int)Math.Floor(Math.Pow(level, 1.43) * 140),
            <= 80 => (int)Math.Floor(Math.Pow(level, 1.4) * 165),
            <= 100 => (int)Math.Floor(Math.Pow(level, 1.38) * 185),
            _ => (int)Math.Floor(Math.Pow(level, 2.5) * 1.13), //violence
        };
    }

    public static bool GainWinningRewards(IHero hero, IEnemy enemy, decimal scaleFactor)
    {
        var boostMultiplier = 1m;
        var effectiveLevel = scaleFactor * enemy.MinLevel;
        if (effectiveLevel > hero.Level)
        {
            boostMultiplier += 0.5m;
        }

        hero.Xp += enemy.BaseExp * boostMultiplier;
        hero.Money += (int)Math.Floor(hero.Xp / 10);
        return hero.Xp >= CalculateXpNeededForLevel(hero.Level);
    }

    public static bool IsValidSlotForItem(Item item, EquipmentSlot slot)
    {
        var itemName = item.Type.ToString();
        return slot switch
        {
            EquipmentSlot.Weapon => itemName.Contains("Sword")
                                    || itemName.Contains("Staff")
                                    || itemName.Contains("Bow"),
            EquipmentSlot.Armor => itemName.Contains("Armor")
                                   || itemName.Contains("Book")
                                   || itemName.Contains("orb"),
            EquipmentSlot.Accessory => itemName.Contains("Shield")
                                       || itemName.Contains("Amulet")
                                       || itemName.Contains("Charm")
                                       || itemName.Contains("Boots"),
            _ => false
        };
    }

    public static void ApplyEquipmentBenefits(this IHero hero, Item item)
    {
        // Apply based on item type using switch expression
        _ = item.Type switch
        {
            // Warrior equipment
            ItemTypes.Warrior_WoodenSword => hero.Dmg += Math.Floor(hero.Dmg * 0.05m),
            ItemTypes.Warrior_IronSword => hero.Dmg += Math.Floor(hero.Dmg * 0.15m),
            ItemTypes.Warrior_SwordOfMarcus => hero.Dmg += Math.Floor(hero.Dmg * 0.3m),
            ItemTypes.Warrior_LeatherArmor => hero.DefencePercentage += 0.03m,
            ItemTypes.Warrior_ChainMailArmor => hero.DefencePercentage += 0.06m,
            ItemTypes.Warrior_PlateArmor => hero.DefencePercentage += 0.12m,
            ItemTypes.Warrior_ArmorOfMarcus => hero.DefencePercentage += 0.2m,
            ItemTypes.Warrior_WoodenShield => hero.DefencePercentage += 0.02m,
            ItemTypes.Warrior_IronShield => hero.DefencePercentage += 0.05m,
            ItemTypes.Warrior_ShieldOfMarcus => hero.DefencePercentage += 0.1m,

            // Archer equipment
            ItemTypes.Archer_QuickBow => hero.Dmg += Math.Floor(hero.Dmg * 0.05m),
            ItemTypes.Archer_LongBow => hero.Dmg += Math.Floor(hero.Dmg * 0.10m),
            ItemTypes.Archer_Crossbow => hero.Dmg += Math.Floor(hero.Dmg * 0.20m),
            ItemTypes.Archer_BowOfTheElves => hero.Dmg += Math.Floor(hero.Dmg * 0.25m),
            ItemTypes.Archer_TraineeArmor => hero.DefencePercentage += 0.02m,
            ItemTypes.Archer_HunterArmor => hero.DefencePercentage += 0.05m,
            ItemTypes.Archer_ArmorOfTheElves => hero.DefencePercentage += 0.1m,
            ItemTypes.Archer_SneakingBoots => 0,
            ItemTypes.Archer_BootsOfTheElves => 0,

            // Mage equipment
            ItemTypes.Mage_WoodenStaff => hero.Dmg += Math.Floor(hero.Dmg * 0.05m),
            ItemTypes.Mage_WizardStaff => hero.Dmg += Math.Floor(hero.Dmg * 0.15m),
            ItemTypes.Mage_StaffOfTheRoyalWizard => hero.Dmg += Math.Floor(hero.Dmg * 0.3m),
            ItemTypes.Mage_StaffOfTheArchmage => hero.Dmg += Math.Floor(hero.Dmg * 0.4m),
            ItemTypes.Mage_ManaAmulet => hero.Mp += 50,
            ItemTypes.Mage_SpellBook => hero.Mp += 120,
            ItemTypes.Mage_OrbOfTheArchmage => hero.Mp += 200,
            ItemTypes.Mage_CharmOfTheWind => hero.Mp += 300,

            _ => throw new Exception("Don't apply an equipment benefit of an item that is not considered 'equipment'")
        };
    }

    public static void RemoveEquipmentBenefits(this IHero hero, Item? item)
    {
        if (item == null) return;

        _ = item.Type switch
        {
            // Warrior equipment
            ItemTypes.Warrior_WoodenSword => hero.Dmg -= Math.Floor(hero.Dmg * 0.05m),
            ItemTypes.Warrior_IronSword => hero.Dmg -= Math.Floor(hero.Dmg * 0.15m),
            ItemTypes.Warrior_SwordOfMarcus => hero.Dmg -= Math.Floor(hero.Dmg * 0.3m),
            ItemTypes.Warrior_LeatherArmor => hero.DefencePercentage -= 0.03m,
            ItemTypes.Warrior_ChainMailArmor => hero.DefencePercentage -= 0.06m,
            ItemTypes.Warrior_PlateArmor => hero.DefencePercentage -= 0.12m,
            ItemTypes.Warrior_ArmorOfMarcus => hero.DefencePercentage -= 0.2m,
            ItemTypes.Warrior_WoodenShield => hero.DefencePercentage -= 0.02m,
            ItemTypes.Warrior_IronShield => hero.DefencePercentage -= 0.05m,
            ItemTypes.Warrior_ShieldOfMarcus => hero.DefencePercentage -= 0.1m,
            // Archer equipment
            ItemTypes.Archer_QuickBow => hero.Dmg -= Math.Floor(hero.Dmg * 0.05m),
            ItemTypes.Archer_LongBow => hero.Dmg -= Math.Floor(hero.Dmg * 0.10m),
            ItemTypes.Archer_Crossbow => hero.Dmg -= Math.Floor(hero.Dmg * 0.20m),
            ItemTypes.Archer_BowOfTheElves => hero.Dmg -= Math.Floor(hero.Dmg * 0.25m),
            ItemTypes.Archer_TraineeArmor => hero.DefencePercentage -= 0.02m,
            ItemTypes.Archer_HunterArmor => hero.DefencePercentage -= 0.05m,
            ItemTypes.Archer_ArmorOfTheElves => hero.DefencePercentage -= 0.1m,
            ItemTypes.Archer_SneakingBoots => 0,
            ItemTypes.Archer_BootsOfTheElves => 0,

            // Mage equipment
            ItemTypes.Mage_WoodenStaff => hero.Dmg -= Math.Floor(hero.Dmg * 0.05m),
            ItemTypes.Mage_WizardStaff => hero.Dmg -= Math.Floor(hero.Dmg * 0.15m),
            ItemTypes.Mage_StaffOfTheRoyalWizard => hero.Dmg -= Math.Floor(hero.Dmg * 0.3m),
            ItemTypes.Mage_StaffOfTheArchmage => hero.Dmg -= Math.Floor(hero.Dmg * 0.4m),
            ItemTypes.Mage_ManaAmulet => hero.Mp -= 50,
            ItemTypes.Mage_SpellBook => hero.Mp -= 120,
            ItemTypes.Mage_OrbOfTheArchmage => hero.Mp -= 200,
            ItemTypes.Mage_CharmOfTheWind => hero.Mp -= 300,

            _ => throw new Exception("Don't remove an equipment benefit of an item that is not considered 'equipment'")
        };
    }

    public static void LevelUp(IHero hero)
    {
        hero.Level++;
        hero.Hp += hero.HpPerLevel;
        hero.Dmg += hero.DmgPerLevel;

        if (hero.DefencePercentage + hero.DefenseIncreasePerLevel < hero.MaxDefense)
        {
            hero.DefencePercentage += hero.DefenseIncreasePerLevel;
        }
    }
}