using Entities.Enemies;
using Entities.Items;

namespace Entities.Heroes;

public class Archer : IHero
{
    // Constants for better maintainability
    private const int BaseHp = 2200;
    private const int BaseDmg = 300;
    private const int BaseMp = 100;
    private const int MpPerLevel = 10;
    private const decimal BaseDefense = 0.01m;
    private const int SpecialMpCost = 30;
    private const int SpecialDuration = 3;
    private const decimal SpecialDamageBoost = 0.5m;
    
    // IHero properties
    public string? Type { get; set; }
    public decimal Hp { get; set; }
    public decimal Dmg { get; set; }
    public decimal Mp { get; set; }
    public decimal DefencePercentage { get; set; }
    public decimal MaxDefense { get; set; } = 0.5m;
    public decimal Xp { get; set; }
    public int Level { get; set; }
    public int TurnCounter { get; set; }
    public int Money { get; set; }
    public Dictionary<EquipmentSlot, Item?> Equipment { get; } = new();
    public bool SpecialActive { get; private set; }
    public decimal DefenseIncreasePerLevel { get; set; } = 0.008m;
    public decimal DmgPerLevel { get; set; } = 30;
    public decimal HpPerLevel { get; set; } = 220;

    // Archer-specific properties
    private decimal _originalDmg;

    public Archer()
    {
        Type = "Archer";
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
        return Dmg;
    }

    public bool GainWinningRewards(IEnemy enemy)
    {
        return this.GainWinningRewards(enemy); // Uses extension method
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
        enemy.Hp -= CalculateActualDamage();
        
        if (TurnCounter > 0) TurnCounter--;
        if (CheckTimer() && SpecialActive) CancelSpecialAbility();
        
        // Special effect for Elven Bow
        var weapon = Equipment[EquipmentSlot.Weapon];
        if (weapon is { Type: ItemTypes.Archer_BowOfTheElves })
        {
            Mp += 3;
            if (Mp > CalculateMaxMp()) Mp = CalculateMaxMp();
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
        _originalDmg = Dmg;
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
        if (SpecialActive)
        {
            Dmg = _originalDmg;
            SpecialActive = false;
        }
    }

    public int CalculateMaxHp() => (int)(BaseHp + HpPerLevel * (Level - 1));
    
    public int CalculateMaxMp() => BaseMp + MpPerLevel * (Level - 1);

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
