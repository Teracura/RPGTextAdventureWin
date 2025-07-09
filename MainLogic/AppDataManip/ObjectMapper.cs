using System.Collections;
using System.Diagnostics;
using Entities.Heroes;
using Entities.Items;
using MainLogic.GlobalParameters;
using MainLogic.GlobalParameters.BaseEntities;

namespace MainLogic.AppDataManip;

public class ObjectMapper
{
    public readonly GameStateParameters Instance = GameStateParameters.Instance;

    public void ConvertDictionaryStats(OwnedItemsBase ownedItemsBase)
    {
        var ownedItems = Instance.OwnedItemsList.Items;
        CopyDictionaryStats(ownedItemsBase, ownedItems);
    }

    public OwnedItemsBase ConvertDictionaryStats(int saveSlot)
    {
        var ownedItemsBase = new OwnedItemsBase();
        CopyDictionaryStats(Instance.OwnedItemsList.Items, ownedItemsBase);
        ownedItemsBase.SaveSlot = saveSlot;
        return ownedItemsBase;
    }

    public HeroBaseEntity ConvertHeroStats(IHero hero, int saveId)
    {
        HeroBaseEntity entity = hero switch
        {
            Warrior => new WarriorEntity(),
            Mage => new MageEntity(),
            Archer => new ArcherEntity(),
            _ => throw new InvalidOperationException("Unknown hero type")
        };

        CopyHeroStatsCommon(hero, entity);
        entity.SaveSlot = saveId;
        return entity;
    }

    public IHero ConvertHeroStats(HeroBaseEntity hero)
    {
        IHero domainHero = hero switch
        {
            WarriorEntity => new Warrior(),
            MageEntity => new Mage(),
            ArcherEntity => new Archer(),
            _ => throw new InvalidOperationException("Unknown hero entity type")
        };

        CopyHeroStatsCommon(hero, domainHero);
        return domainHero;
    }

    public GameStateParametersBaseEntity ConvertGameStateParameters(int saveId)
    {
        var state = GameStateParameters.Instance;
        var stateBase = CopyGameStateParametersCommon(state);
        stateBase!.SaveSlot = saveId; //won't be null trust me bro
        return stateBase;
    }

    public void ConvertGameStateParameters(GameStateParametersBaseEntity stateBase)
    {
        var state = GameStateParameters.Instance;

        CopyGameStateParametersCommon(stateBase, state);
    }

    private GameStateParametersBaseEntity? CopyGameStateParametersCommon(object source, object? target = null)
    {
        switch ((source, target))
        {
            case (GameStateParameters src, null):
                return new GameStateParametersBaseEntity(
                    saveSlot: -1, // will be reassigned later
                    saving: src.Saving,
                    isDefeated: src.HeroState.IsDefeated,
                    equippedWeapon: src.HeroState.EquippedWeapon,
                    equippedArmor: src.HeroState.EquippedArmor,
                    equippedAccessory: src.HeroState.EquippedAccessory,
                    scaleFactor: src.MetaProgressionState.ScaleFactor,
                    globalEnemiesKilled: src.MetaProgressionState.GlobalEnemiesKilled,
                    globalTimesDefeated: src.MetaProgressionState.GlobalTimesDefeated,
                    globalDungeonsCleared: src.MetaProgressionState.GlobalDungeonsCleared,
                    kaitoKeyEffect: src.MetaProgressionState.KaitoKeyEffect,
                    numberOfEnemiesPerDungeon: src.DungeonState.NumberOfEnemiesPerDungeon,
                    numberOfEnemiesDefeated: src.DungeonState.NumberOfEnemiesDefeated,
                    enemyDefeated: src.DungeonState.EnemyDefeated,
                    dungeonCleared: src.DungeonState.DungeonCleared
                );
            case (GameStateParametersBaseEntity src, GameStateParameters dest):
                dest.Saving = src.Saving;
                dest.DungeonState.DungeonCleared = src.DungeonCleared;
                dest.MetaProgressionState.GlobalTimesDefeated = src.GlobalTimesDefeated;
                dest.MetaProgressionState.GlobalEnemiesKilled = src.GlobalEnemiesKilled;
                dest.MetaProgressionState.GlobalDungeonsCleared = src.GlobalDungeonsCleared;
                dest.MetaProgressionState.KaitoKeyEffect = src.KaitoKeyEffect;
                dest.MetaProgressionState.ScaleFactor = src.ScaleFactor;
                dest.DungeonState.NumberOfEnemiesDefeated = src.NumberOfEnemiesDefeated;
                dest.DungeonState.NumberOfEnemiesPerDungeon = src.NumberOfEnemiesPerDungeon;
                dest.DungeonState.EnemyDefeated = src.EnemyDefeated;
                dest.HeroState.IsDefeated = src.IsDefeated;
                dest.HeroState.EquippedAccessory =
                    ItemCatalog.GetItemData(src.EquippedAccessory)?.Type ?? ItemTypes.Nothing;
                dest.HeroState.EquippedArmor = ItemCatalog.GetItemData(src.EquippedArmor)?.Type ?? ItemTypes.Nothing;
                dest.HeroState.EquippedWeapon = ItemCatalog.GetItemData(src.EquippedWeapon)?.Type ?? ItemTypes.Nothing;
                break;
            default:
                throw new InvalidOperationException("Unsupported mapping types.");
        }

        return null;
    }

    private void CopyHeroStatsCommon(object source, object target)
    {
        switch ((source, target))
        {
            case (IHero src, HeroBaseEntity dest):
                dest.Type = src.Type;
                dest.Hp = src.Hp;
                dest.Dmg = src.Dmg;
                dest.Mp = src.Mp;
                dest.DefencePercentage = src.DefencePercentage;
                dest.MaxDefense = src.MaxDefense;
                dest.Xp = src.Xp;
                dest.Level = src.Level;
                dest.Money = src.Money;
                dest.DefenseIncreasePerLevel = src.DefenseIncreasePerLevel;
                dest.DmgPerLevel = src.DmgPerLevel;
                dest.HpPerLevel = src.HpPerLevel;
                break;

            case (HeroBaseEntity src, IHero dest):
                dest.Type = src.Type;
                dest.Hp = src.Hp;
                dest.Dmg = src.Dmg;
                dest.Mp = src.Mp;
                dest.DefencePercentage = src.DefencePercentage;
                dest.MaxDefense = src.MaxDefense;
                dest.Xp = src.Xp;
                dest.Level = src.Level;
                dest.Money = src.Money;
                dest.DefenseIncreasePerLevel = src.DefenseIncreasePerLevel;
                dest.DmgPerLevel = src.DmgPerLevel;
                dest.HpPerLevel = src.HpPerLevel;
                break;

            default:
                throw new InvalidOperationException("Unsupported mapping types.");
        }
    }

    private void CopyDictionaryStats(object source, object target)
    {
        switch ((source, target))
        {
            case (Dictionary<ItemTypes, int> src, OwnedItemsBase dest):
                // Common
                dest.Common_SmallHealingPotion = src.GetValueOrDefault(ItemTypes.Common_SmallHealingPotion);
                dest.Common_BigHealingPotion = src.GetValueOrDefault(ItemTypes.Common_BigHealingPotion);
                dest.Common_BlessingOfLife = src.GetValueOrDefault(ItemTypes.Common_BlessingOfLife);
                dest.Common_SmallManaPotion = src.GetValueOrDefault(ItemTypes.Common_SmallManaPotion);
                dest.Common_BigManaPotion = src.GetValueOrDefault(ItemTypes.Common_BigManaPotion);
                dest.Common_BlessingOfTheMagician = src.GetValueOrDefault(ItemTypes.Common_BlessingOfTheMagician);
                dest.Common_KaitoKey = src.GetValueOrDefault(ItemTypes.Common_KaitoKey);
                //Warrior
                dest.Warrior_WoodenSword = src.GetValueOrDefault(ItemTypes.Warrior_WoodenSword);
                dest.Warrior_IronSword = src.GetValueOrDefault(ItemTypes.Warrior_IronSword);
                dest.Warrior_SwordOfMarcus = src.GetValueOrDefault(ItemTypes.Warrior_SwordOfMarcus);
                dest.Warrior_LeatherArmor = src.GetValueOrDefault(ItemTypes.Warrior_LeatherArmor);
                dest.Warrior_ChainMailArmor = src.GetValueOrDefault(ItemTypes.Warrior_ChainMailArmor);
                dest.Warrior_PlateArmor = src.GetValueOrDefault(ItemTypes.Warrior_PlateArmor);
                dest.Warrior_ArmorOfMarcus = src.GetValueOrDefault(ItemTypes.Warrior_ArmorOfMarcus);
                dest.Warrior_WoodenShield = src.GetValueOrDefault(ItemTypes.Warrior_WoodenShield);
                dest.Warrior_IronShield = src.GetValueOrDefault(ItemTypes.Warrior_IronShield);
                dest.Warrior_ShieldOfMarcus = src.GetValueOrDefault(ItemTypes.Warrior_ShieldOfMarcus);
                // Archer
                dest.Archer_QuickBow = src.GetValueOrDefault(ItemTypes.Archer_QuickBow);
                dest.Archer_LongBow = src.GetValueOrDefault(ItemTypes.Archer_LongBow);
                dest.Archer_Crossbow = src.GetValueOrDefault(ItemTypes.Archer_Crossbow);
                dest.Archer_BowOfTheElves = src.GetValueOrDefault(ItemTypes.Archer_BowOfTheElves);
                dest.Archer_TraineeArmor = src.GetValueOrDefault(ItemTypes.Archer_TraineeArmor);
                dest.Archer_HunterArmor = src.GetValueOrDefault(ItemTypes.Archer_HunterArmor);
                dest.Archer_ArmorOfTheElves = src.GetValueOrDefault(ItemTypes.Archer_ArmorOfTheElves);
                dest.Archer_SneakingBoots = src.GetValueOrDefault(ItemTypes.Archer_SneakingBoots);
                dest.Archer_BootsOfTheElves = src.GetValueOrDefault(ItemTypes.Archer_BootsOfTheElves);
                // Mage
                dest.Mage_WoodenStaff = src.GetValueOrDefault(ItemTypes.Mage_WoodenStaff);
                dest.Mage_WizardStaff = src.GetValueOrDefault(ItemTypes.Mage_WizardStaff);
                dest.Mage_StaffOfTheRoyalWizard = src.GetValueOrDefault(ItemTypes.Mage_StaffOfTheRoyalWizard);
                dest.Mage_StaffOfTheArchmage = src.GetValueOrDefault(ItemTypes.Mage_StaffOfTheArchmage);
                dest.Mage_ManaAmulet = src.GetValueOrDefault(ItemTypes.Mage_ManaAmulet);
                dest.Mage_SpellBook = src.GetValueOrDefault(ItemTypes.Mage_SpellBook);
                dest.Mage_OrbOfTheArchmage = src.GetValueOrDefault(ItemTypes.Mage_OrbOfTheArchmage);
                dest.Mage_CharmOfTheWind = src.GetValueOrDefault(ItemTypes.Mage_CharmOfTheWind);
                Debug.WriteLine("=== Loaded items ===");
                foreach (var item in src)
                {
                    Debug.WriteLine($"{item.Key}: {item.Value}");
                }

                break;

            case (OwnedItemsBase src, Dictionary<ItemTypes, int> dest):
                // Common
                dest[ItemTypes.Common_SmallHealingPotion] = src.Common_SmallHealingPotion;
                dest[ItemTypes.Common_BigHealingPotion] = src.Common_BigHealingPotion;
                dest[ItemTypes.Common_BlessingOfLife] = src.Common_BlessingOfLife;
                dest[ItemTypes.Common_SmallManaPotion] = src.Common_SmallManaPotion;
                dest[ItemTypes.Common_BigManaPotion] = src.Common_BigManaPotion;
                dest[ItemTypes.Common_BlessingOfTheMagician] = src.Common_BlessingOfTheMagician;
                dest[ItemTypes.Common_KaitoKey] = src.Common_KaitoKey;
                // Warrior
                dest[ItemTypes.Warrior_WoodenSword] = src.Warrior_WoodenSword;
                dest[ItemTypes.Warrior_IronSword] = src.Warrior_IronSword;
                dest[ItemTypes.Warrior_SwordOfMarcus] = src.Warrior_SwordOfMarcus;
                dest[ItemTypes.Warrior_LeatherArmor] = src.Warrior_LeatherArmor;
                dest[ItemTypes.Warrior_ChainMailArmor] = src.Warrior_ChainMailArmor;
                dest[ItemTypes.Warrior_PlateArmor] = src.Warrior_PlateArmor;
                dest[ItemTypes.Warrior_ArmorOfMarcus] = src.Warrior_ArmorOfMarcus;
                dest[ItemTypes.Warrior_WoodenShield] = src.Warrior_WoodenShield;
                dest[ItemTypes.Warrior_IronShield] = src.Warrior_IronShield;
                dest[ItemTypes.Warrior_ShieldOfMarcus] = src.Warrior_ShieldOfMarcus;
                // Archer
                dest[ItemTypes.Archer_QuickBow] = src.Archer_QuickBow;
                dest[ItemTypes.Archer_LongBow] = src.Archer_LongBow;
                dest[ItemTypes.Archer_Crossbow] = src.Archer_Crossbow;
                dest[ItemTypes.Archer_BowOfTheElves] = src.Archer_BowOfTheElves;
                dest[ItemTypes.Archer_TraineeArmor] = src.Archer_TraineeArmor;
                dest[ItemTypes.Archer_HunterArmor] = src.Archer_HunterArmor;
                dest[ItemTypes.Archer_ArmorOfTheElves] = src.Archer_ArmorOfTheElves;
                dest[ItemTypes.Archer_SneakingBoots] = src.Archer_SneakingBoots;
                dest[ItemTypes.Archer_BootsOfTheElves] = src.Archer_BootsOfTheElves;
                // Mage
                dest[ItemTypes.Mage_WoodenStaff] = src.Mage_WoodenStaff;
                dest[ItemTypes.Mage_WizardStaff] = src.Mage_WizardStaff;
                dest[ItemTypes.Mage_StaffOfTheRoyalWizard] = src.Mage_StaffOfTheRoyalWizard;
                dest[ItemTypes.Mage_StaffOfTheArchmage] = src.Mage_StaffOfTheArchmage;
                dest[ItemTypes.Mage_ManaAmulet] = src.Mage_ManaAmulet;
                dest[ItemTypes.Mage_SpellBook] = src.Mage_SpellBook;
                dest[ItemTypes.Mage_OrbOfTheArchmage] = src.Mage_OrbOfTheArchmage;
                dest[ItemTypes.Mage_CharmOfTheWind] = src.Mage_CharmOfTheWind;
                break;

            default:
                throw new InvalidOperationException("Unsupported mapping types.");
        }
    }
}