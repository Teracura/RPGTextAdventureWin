using System.ComponentModel.DataAnnotations;

namespace Entities.Heroes;

public abstract class HeroBaseEntity
{
    [Key]
    public int SaveSlot { get; set; }
    public string? Type { get; set; }
    public decimal Hp { get; set; }
    public decimal Dmg { get; set; }
    public decimal Mp { get; set; }
    public decimal DefencePercentage { get; set; }
    public decimal MaxDefense { get; set; }
    public decimal Xp { get; set; }
    public int Level { get; set; }
    public int Money { get; set; }

    public decimal DefenseIncreasePerLevel { get; set; }
    public decimal DmgPerLevel { get; set; }
    public decimal HpPerLevel { get; set; }
}

public class WarriorEntity : HeroBaseEntity { }

public class MageEntity : HeroBaseEntity { }

public class ArcherEntity : HeroBaseEntity { }