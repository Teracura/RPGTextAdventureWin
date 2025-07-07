namespace Entities.Items
{
    public static class ItemCatalog
    {
        private static readonly Dictionary<ItemTypes, Item> Items = new()
        {
            // Common Items
            {
                ItemTypes.Common_SmallHealingPotion,
                new Item(
                    ItemTypes.Common_SmallHealingPotion,
                    "Small Healing Potion",
                    "Restores a small amount of health",
                    100,
                    "common"
                )
            },
            {
                ItemTypes.Common_BigHealingPotion,
                new Item(
                    ItemTypes.Common_BigHealingPotion,
                    "Big Healing Potion",
                    "Restores a large amount of health",
                    300,
                    "common"
                )
            },
            {
                ItemTypes.Common_BlessingOfLife,
                new Item(
                    ItemTypes.Common_BlessingOfLife,
                    "Blessing of Life",
                    "Restores the maximum health possible",
                    800,
                    "common"
                )
            },
            {
                ItemTypes.Common_SmallManaPotion,
                new Item(
                    ItemTypes.Common_SmallManaPotion,
                    "Small Mana Potion",
                    "Restores a small amount of mana",
                    100,
                    "common"
                )
            },
            {
                ItemTypes.Common_BigManaPotion,
                new Item(
                    ItemTypes.Common_BigManaPotion,
                    "Big Mana Potion",
                    "Restores a large amount of mana",
                    300,
                    "common"
                )
            },
            {
                ItemTypes.Common_BlessingOfTheMagician,
                new Item(
                    ItemTypes.Common_BlessingOfTheMagician,
                    "Blessing of the Magician",
                    "Restores the maximum mana possible",
                    700,
                    "common"
                )
            },
            {
                ItemTypes.Common_KaitoKey,
                new Item(
                    ItemTypes.Common_KaitoKey,
                    "Dungeon Key",
                    "Required to access special dungeons",
                    10000,
                    "common"
                )
            },

            // Warrior Specific Items
            {
                ItemTypes.Warrior_WoodenSword,
                new Item(
                    ItemTypes.Warrior_WoodenSword,
                    "Wooden Sword",
                    "Basic warrior weapon providing +5% damage",
                    500,
                    "warrior_weapon"
                )
            },
            {
                ItemTypes.Warrior_IronSword,
                new Item(
                    ItemTypes.Warrior_IronSword,
                    "Iron Sword",
                    "Medium-grade warrior weapon providing +15% damage",
                    1000,
                    "warrior_weapon"
                )
            },
            {
                ItemTypes.Warrior_SwordOfMarcus,
                new Item(
                    ItemTypes.Warrior_SwordOfMarcus,
                    "Sword of Marcus",
                    "Legendary sword that belonged to the hero Marcus. Provides +30% damage",
                    4000,
                    "warrior_weapon"
                )
            },
            {
                ItemTypes.Warrior_LeatherArmor,
                new Item(
                    ItemTypes.Warrior_LeatherArmor,
                    "Leather Armor",
                    "Basic armor that provides +3% defense",
                    600,
                    "warrior_armor"
                )
            },
            {
                ItemTypes.Warrior_ChainMailArmor,
                new Item(
                    ItemTypes.Warrior_ChainMailArmor,
                    "Chain Mail Armor",
                    "Medium-grade armor that provides +6% defense",
                    1100,
                    "warrior_armor"
                )
            },
            {
                ItemTypes.Warrior_PlateArmor,
                new Item(
                    ItemTypes.Warrior_PlateArmor,
                    "Plate Armor",
                    "Heavy armor providing +12% defense",
                    2000,
                    "warrior_armor"
                )
            },
            {
                ItemTypes.Warrior_ArmorOfMarcus,
                new Item(
                    ItemTypes.Warrior_ArmorOfMarcus,
                    "Armor of Marcus",
                    "Legendary armor that provides +20% defense and 2% life steal",
                    5000,
                    "warrior_armor"
                )
            },
            {
                ItemTypes.Warrior_WoodenShield,
                new Item(
                    ItemTypes.Warrior_WoodenShield,
                    "Wooden Shield",
                    "Basic shield that provides +2% defense",
                    400,
                    "warrior_accessory"
                )
            },
            {
                ItemTypes.Warrior_IronShield,
                new Item(
                    ItemTypes.Warrior_IronShield,
                    "Iron Shield",
                    "Medium-grade shield that provides +5% defense",
                    1500,
                    "warrior_accessory"
                )
            },
            {
                ItemTypes.Warrior_ShieldOfMarcus,
                new Item(
                    ItemTypes.Warrior_ShieldOfMarcus,
                    "Shield of Marcus",
                    "Legendary shield providing +10% defense and 2% chance to block attacks",
                    5000,
                    "warrior_accessory"
                )
            },

            // Archer Specific Items
            {
                ItemTypes.Archer_QuickBow,
                new Item(
                    ItemTypes.Archer_QuickBow,
                    "Quick Bow",
                    "Basic bow that increases attack damage by 5%",
                    400,
                    "archer_weapon"
                )
            },
            {
                ItemTypes.Archer_LongBow,
                new Item(
                    ItemTypes.Archer_LongBow,
                    "Long Bow",
                    "Medium-range bow providing +10% damage",
                    1000,
                    "archer_weapon"
                )
            },
            {
                ItemTypes.Archer_Crossbow,
                new Item(
                    ItemTypes.Archer_Crossbow,
                    "Crossbow",
                    "Powerful weapon with +20% damage",
                    2500,
                    "archer_weapon"
                )
            },
            {
                ItemTypes.Archer_BowOfTheElves,
                new Item(
                    ItemTypes.Archer_BowOfTheElves,
                    "Bow of the Elves",
                    "Legendary elven bow providing +25% damage",
                    4000,
                    "archer_weapon"
                )
            },
            {
                ItemTypes.Archer_TraineeArmor,
                new Item(
                    ItemTypes.Archer_TraineeArmor,
                    "Trainee Armor",
                    "Basic light armor for archers providing +2% defense",
                    500,
                    "archer_armor"
                )
            },
            {
                ItemTypes.Archer_HunterArmor,
                new Item(
                    ItemTypes.Archer_HunterArmor,
                    "Hunter Armor",
                    "Medium-grade leather armor providing +5% defense",
                    1000,
                    "archer_armor"
                )
            },
            {
                ItemTypes.Archer_ArmorOfTheElves,
                new Item(
                    ItemTypes.Archer_ArmorOfTheElves,
                    "Armor of the Elves",
                    "Legendary elven armor providing +10% defense",
                    2000,
                    "archer_armor"
                )
            },
            {
                ItemTypes.Archer_SneakingBoots,
                new Item(
                    ItemTypes.Archer_SneakingBoots,
                    "Sneaking Boots",
                    "Special boots that give a 2% chance to dodge attacks",
                    700,
                    "archer_accessory"
                )
            },
            {
                ItemTypes.Archer_BootsOfTheElves,
                new Item(
                    ItemTypes.Archer_BootsOfTheElves,
                    "Sneakers of the Elves",
                    "Legendary elven boots providing 20% chance to dodge attacks",
                    10000,
                    "archer_accessory"
                )
            },

            // Mage Specific Items
            {
                ItemTypes.Mage_WoodenStaff,
                new Item(
                    ItemTypes.Mage_WoodenStaff,
                    "Wooden Staff",
                    "Basic mage staff providing +5% damage",
                    700,
                    "mage_weapon"
                )
            },
            {
                ItemTypes.Mage_WizardStaff,
                new Item(
                    ItemTypes.Mage_WizardStaff,
                    "Wizard Staff",
                    "Medium-grade staff providing +15% damage",
                    1500,
                    "mage_weapon"
                )
            },
            {
                ItemTypes.Mage_StaffOfTheRoyalWizard,
                new Item(
                    ItemTypes.Mage_StaffOfTheRoyalWizard,
                    "Staff of the Royal Wizard",
                    "High-grade staff of the King's wizard providing +25% and -10% spell cost",
                    3000,
                    "mage_weapon"
                )
            },
            {
                ItemTypes.Mage_StaffOfTheArchmage,
                new Item(
                    ItemTypes.Mage_StaffOfTheArchmage,
                    "Staff of the Archmage",
                    "Legendary staff providing +40% damage and -25% spell cost",
                    12000,
                    "mage_weapon"
                )
            },
            {
                ItemTypes.Mage_ManaAmulet,
                new Item(
                    ItemTypes.Mage_ManaAmulet,
                    "Mana Amulet",
                    "increases mana by 50",
                    300,
                    "mage_hybrid"
                )
            },
            {
                ItemTypes.Mage_SpellBook,
                new Item(
                    ItemTypes.Mage_SpellBook,
                    "Spell Book",
                    "increases mana by 120",
                    1500,
                    "mage_hybrid"
                )
            },
            {
                ItemTypes.Mage_OrbOfTheArchmage,
                new Item(
                    ItemTypes.Mage_OrbOfTheArchmage,
                    "Orb of the Archmage",
                    "increases mana by 200 and decreases spell cost by 15%",
                    8000,
                    "mage_hybrid"
                )
            },
            {
                ItemTypes.Mage_CharmOfTheWind,
                new Item(
                    ItemTypes.Mage_CharmOfTheWind,
                    "Charm of the Wind",
                    """
                    An Ancient charm that protects its wearer,
                    increases mana by 300 and decreases spell cost by 25%
                    and has a 10% chance of decreasing incoming damage by 90%
                    """,
                    30000,
                    "mage_hybrid"
                )
            }
        };

        public static Item? GetItemData(ItemTypes type)
        {
            if (type == ItemTypes.Nothing) return null;
            return Items.TryGetValue(type, out var item) ? item : null;
        }


        public static Item GetItemData(string type)
        {
            if (!Enum.TryParse<ItemTypes>(type, ignoreCase: true, out var itemType))
            {
                throw new ArgumentException($"Invalid item type: {type}");
            }

            if (Items.TryGetValue(itemType, out var itemData))
            {
                return itemData;
            }

            throw new ArgumentException($"No data available for item: {type}");
        }

        public static IEnumerable<Item> GetItemsForHero(string heroType)
        {
            return Items.Values.Where(item => item.CanBeUsedBy(heroType));
        }

        public static Item? GetItemData(ItemTypes? type)
        {
            if (type == null) return null;
            return GetItemData(type);
        }
    }
}