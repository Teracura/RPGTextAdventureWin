using System.Text.Json.Serialization;
using Entities.Enemies;
using Entities.Items;

namespace Entities.Heroes;

[JsonDerivedType(typeof(Warrior), typeDiscriminator: "Warrior")]
[JsonDerivedType(typeof(Mage), typeDiscriminator: "Mage")]
[JsonDerivedType(typeof(Archer), typeDiscriminator: "Archer")]
public interface IHero
{
    string? Type { get; set; }
    decimal Hp { get; set; }
    decimal Dmg { get; set; }
    decimal Mp { get; set; }
    decimal DefencePercentage { get; set; }
    decimal MaxDefense { get; set; }
    decimal Xp { get; set; }
    int Level { get; set; }
    int TurnCounter { get; set; }
    int Money { get; set; }
    
    Dictionary<EquipmentSlot, Item?> Equipment { get; }
    decimal DefenseIncreasePerLevel { get; set; }
    decimal DmgPerLevel { get; set; }
    decimal HpPerLevel { get; set; }

    decimal CalculateActualDamage();
    void RestoreHealth();
    void RestoreMp();
    void Rest();
    bool Attack(IEnemy? enemy);
    bool SpecialAbility();
    bool CheckTimer();
    void CancelSpecialAbility();
    int CalculateMaxHp();
    int CalculateMaxMp();
    bool EquipItem(Item item, EquipmentSlot slot);
    void UnequipItem(EquipmentSlot slot);
}