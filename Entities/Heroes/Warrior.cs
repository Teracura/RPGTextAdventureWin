using Entities.Enemies;
using Entities.Items;

namespace Entities.Heroes;

public class Warrior : IHero
{
    // Constants for better maintainability
    private const int BaseHp = 2500;
    private const int BaseDmg = 200;
    private const int BaseMp = 50;
    private const int MpPerLevel = 5;
    private const decimal BaseDefense = 0.01m;
    private const int SpecialMpCost = 10;
    private const int SpecialDuration = 3;
    private const decimal SpecialDefenseNerf = -0.1m;
    private const decimal SpecialDamageBoost = 0.3m;
    
    // IHero properties
    public string? Type { get; set; }
    public decimal Hp { get; set; }
    public decimal Dmg { get; set; }
    public decimal Mp { get; set; }
    public decimal DefencePercentage { get; set; }
    public decimal MaxDefense { get; set; } = 0.6m;
    public decimal Xp { get; set; }
    public int Level { get; set; }
    public int TurnCounter { get; set; }
    public int Money { get; set; }
    public Dictionary<EquipmentSlot, Item?> Equipment { get; } = new();
    public bool SpecialActive { get; private set; }
    public decimal DefenseIncreasePerLevel { get; set; } = 0.01m;
    public decimal DmgPerLevel { get; set; } = 20;
    public decimal HpPerLevel { get; set; } = 250;

    public Warrior()
    {
        Type = "Warrior";
        Hp = BaseHp;
        Dmg = BaseDmg;
        Mp = BaseMp;
        DefencePercentage = BaseDefense;
        Level = 1;
        Money = 100;
        
        // Initialize equipment slots
        Equipment[EquipmentSlot.Weapon] = null;
        Equipment[EquipmentSlot.Armor] = null;
        Equipment[EquipmentSlot.Accessory] = null;
    }

    public decimal CalculateActualDamage()
    {
        return SpecialActive ? Dmg : Dmg;
    }

    public decimal CalculateXpNeededForLevel(int level)
    {
        return HeroUtils.CalculateXpNeededForLevel(level);
    }

    public void RestoreHealth()
    {
        Hp = CalculateMaxHp();
    }

    public void RestoreMp()
    {
        Mp = CalculateMaxMp();
    }

    public void Rest()
    {
        Hp = CalculateMaxHp();
        Mp = CalculateMaxMp();
    }

    public bool Attack(IEnemy? enemy)
    {
        var weapon = Equipment[EquipmentSlot.Weapon];
        var shield = Equipment[EquipmentSlot.Accessory];
        var armor = Equipment[EquipmentSlot.Armor];
        
        enemy.Hp -= CalculateActualDamage();
        
        if (TurnCounter > 0) TurnCounter--;
        if (CheckTimer() && SpecialActive) CancelSpecialAbility();
        
        if (weapon is { Type: ItemTypes.Warrior_SwordOfMarcus })
        {
            Hp += Dmg * 0.02m;
            if (Hp > CalculateMaxHp()) Hp = CalculateMaxHp();
        }
        
        return enemy.Hp <= 0;
    }

    public bool SpecialAbility()
    {
        if (Mp < SpecialMpCost || !CheckTimer())
        {
            return false;
        }
        
        TurnCounter = SpecialDuration;
        Mp -= SpecialMpCost;
        DefencePercentage += SpecialDefenseNerf;
        Dmg += Dmg * SpecialDamageBoost;
        SpecialActive = true;
        return true;
    }

    public bool CheckTimer()
    {
        return TurnCounter == 0;
    }

    public void CancelSpecialAbility()
    {
        DefencePercentage -= SpecialDefenseNerf;
        Dmg = CalculatePreAbilityDamage();
        SpecialActive = false;
    }

    public int CalculateMaxHp() => (int)(BaseHp + HpPerLevel * (Level - 1));
    
    public int CalculateMaxMp() => BaseMp + MpPerLevel * (Level - 1);
    
    private int CalculatePreAbilityDamage() => (int)(BaseDmg + (Level * DmgPerLevel));

    public bool EquipItem(Item item, EquipmentSlot slot)
    {
        // Check if the item can be used by this hero type
        if (Type != null && !item.CanBeUsedBy(Type)) return false;

        // Check the item type matches the slot
        if (!HeroUtils.IsValidSlotForItem(item, slot)) return false;

        // Get the current item in the slot (if any)
        if (Equipment.TryGetValue(slot, out var currentItem) && currentItem != null)
        {
            this.RemoveItemBenefits(currentItem);
        }

        // Set the new item
        Equipment[slot] = item;

        // Apply the item's benefits
        this.ApplyItemBenefits(item);

        return true;
    }

    public void UnequipItem(EquipmentSlot slot)
    {
        if (!Equipment.TryGetValue(slot, out var item) || item == null) return;

        // Remove the item's benefits
        this.RemoveItemBenefits(item);

        Equipment[slot] = null;
    }
}