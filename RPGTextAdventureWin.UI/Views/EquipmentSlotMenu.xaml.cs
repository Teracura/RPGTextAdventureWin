using System.Collections.ObjectModel;
using Entities.Heroes;
using Entities.Items;
using MainLogic.GlobalParameters;
using RPGTextAdventureWin.Models;

namespace RPGTextAdventureWin.Views;

public partial class EquipmentSlotMenu
{
    public ObservableCollection<InventoryShowViewModel> EquippedItems { get; set; }
    public ObservableCollection<InventoryShowViewModel> EquipmentSlots { get; set; }
    public readonly GameStateParameters Instance = GameStateParameters.Instance;

    public EquipmentSlotMenu()
    {
        InitializeComponent();
        var weaponType = Instance.HeroState.EquippedWeapon;
        var armorType = Instance.HeroState.EquippedArmor;
        var accessoryType = Instance.HeroState.EquippedAccessory;
        EquippedItems = [];
        EquipmentSlots = [];

        var weaponItem = SafeGetItem(weaponType);
        EquippedItems.Add(
            weaponType != ItemTypes.Nothing
                ? new InventoryShowViewModel(
                    weaponItem.Name,
                    weaponItem.Description,
                    "Unequip",
                    new Command(() => UnequipItem("weapon")))
                : new InventoryShowViewModel("No item equipped", "", "Unequip",
                    new Command(() => UnequipItem("weapon")))
        );
        var armorItem = SafeGetItem(armorType);
        EquippedItems.Add(
            armorType != ItemTypes.Nothing
                ? new InventoryShowViewModel(
                    armorItem.Name,
                    armorItem.Description,
                    "Unequip",
                    new Command(() => UnequipItem("armor")))
                : new InventoryShowViewModel("No item equipped", "", "Unequip", new Command(() => UnequipItem("armor")))
        );
        var accessoryItem = SafeGetItem(accessoryType);
        EquippedItems.Add(
            accessoryType != ItemTypes.Nothing
                ? new InventoryShowViewModel(
                    accessoryItem.Name,
                    accessoryItem.Description,
                    "Unequip",
                    new Command(() => UnequipItem("accessory")))
                : new InventoryShowViewModel("No item equipped", "", "Unequip",
                    new Command(() => UnequipItem("accessory")))
        );

        foreach (var item in from pair in Instance.OwnedItemsList.Items
                 where pair.Value > 0
                 where !pair.Key.ToString().StartsWith("Common_")
                 select ItemCatalog.GetItemData(pair.Key))
        {
            EquipmentSlots.Add(new InventoryShowViewModel(item!.Name, item.Description, "equip",
                new Command(() => EquipItem(item)))); //there won't be null items
        }

        BindingContext = this;
    }

    private void EquipItem(Item item)
    {
        var category = item.EquipmentCategory.ToLower();
        if (category.EndsWith("weapon"))
        {
            UnequipItem("weapon");
            Instance.HeroState.EquippedWeapon = item.Type;
            EquippedItems[0].ItemName = item.Name;
            EquippedItems[0].ItemDescription = item.Description;
        }
        else if (category.EndsWith("armor"))
        {
            UnequipItem("armor");
            Instance.HeroState.EquippedArmor = item.Type;
            EquippedItems[1].ItemName = item.Name;
            EquippedItems[1].ItemDescription = item.Description;
        }
        else if (category.EndsWith("accessory"))
        {
            UnequipItem("accessory");
            Instance.HeroState.EquippedAccessory = item.Type;
            EquippedItems[2].ItemName = item.Name;
            EquippedItems[2].ItemDescription = item.Description;
        }
        else if (category.EndsWith("hybrid")) //hybrid is just an item that is both armor and accessory, mage unique
        {
            if (Instance.HeroState.EquippedArmor == ItemTypes.Nothing)
            {
                UnequipItem("armor");
                Instance.HeroState.EquippedArmor = item.Type;
                EquippedItems[1].ItemName = item.Name;
                EquippedItems[1].ItemDescription = item.Description;
            }
            else
            {
                UnequipItem("accessory");
                Instance.HeroState.EquippedAccessory = item.Type;
                EquippedItems[2].ItemName = item.Name;
                EquippedItems[2].ItemDescription = item.Description;
            }
        }

        Instance.HeroState.Hero.ApplyEquipmentBenefits(item);
        if (--Instance.OwnedItemsList.Items[item.Type] == 0)
        {
            EquipmentSlots.Remove(EquipmentSlots.First(slot => slot.ItemName == item.Name));
        }
    }

    private void UnequipItem(string equipment)
    {
        var unequippedType = equipment.ToLower() switch
        {
            "weapon" => Instance.HeroState.EquippedWeapon,
            "armor" => Instance.HeroState.EquippedArmor,
            "accessory" => Instance.HeroState.EquippedAccessory,
            _ => throw new ArgumentOutOfRangeException(nameof(equipment), equipment, null)
        };

        // Clear equipped slot
        switch (equipment.ToLower())
        {
            case "weapon":
                Instance.HeroState.EquippedWeapon = ItemTypes.Nothing;
                EquippedItems[0].ItemName = "No item equipped";
                EquippedItems[0].ItemDescription = "";
                break;
            case "armor":
                Instance.HeroState.EquippedArmor = ItemTypes.Nothing;
                EquippedItems[1].ItemName = "No item equipped";
                EquippedItems[1].ItemDescription = "";
                break;
            case "accessory":
                Instance.HeroState.EquippedAccessory = ItemTypes.Nothing;
                EquippedItems[2].ItemName = "No item equipped";
                EquippedItems[2].ItemDescription = "";
                break;
        }

        if (unequippedType == ItemTypes.Nothing) return;

        Instance.HeroState.Hero.RemoveEquipmentBenefits(ItemCatalog.GetItemData(unequippedType));
        if (Instance.OwnedItemsList.Items[unequippedType]++ != 0) return;

        var item = ItemCatalog.GetItemData(unequippedType);
        if (item == null) return; // Prevent crash

        EquipmentSlots.Add(new InventoryShowViewModel(
            item.Name,
            item.Description,
            "equip",
            new Command(() => EquipItem(item))
        ));
    }


    private async void ReturnToMainMenuClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GameLoopMenu));
    }

    private static Item SafeGetItem(ItemTypes type)
    {
        return ItemCatalog.GetItemData(type) ?? new Item { Name = "No Item equipped", Description = "" };
    }
}