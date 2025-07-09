using Entities.Enemies;
using Entities.Items;

namespace Entities.Heroes;

public class Mage : IHero
{
    // Constants for better maintainability
    private const int BaseHp = 1800;
    private const int BaseDmg = 400;
    private const int BaseMp = 300;
    private const int MpPerLevel = 30;
    private const decimal BaseDefense = 0.01m;
    
    // IHero properties
    public decimal MaxDefense { get; set; } = 0.4m;
    public string? Type { get; set; }
    public decimal Hp { get; set; }
    public decimal Dmg { get; set; }
    public decimal Mp { get; set; }
    public decimal DefencePercentage { get; set; }
    public decimal Xp { get; set; }
    public int Level { get; set; }
    public int Money { get; set; }
    public decimal DefenseIncreasePerLevel { get; set; } = 0.005m;
    public decimal DmgPerLevel { get; set; } = 40m;
    public decimal HpPerLevel { get; set; } = 180m;

    public Mage()
    {
        Type = "Mage";
        Hp = BaseHp;
        Dmg = BaseDmg;
        Mp = BaseMp;
        DefencePercentage = BaseDefense;
        Level = 1;
        Money = 100;
    }

    public decimal CalculateActualDamage()
    {
        return Dmg;
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
        
        return enemy.Hp <= 0;
    }
    public int CalculateMaxHp() => (int)(BaseHp + HpPerLevel * (Level - 1));
    
    public int CalculateMaxMp() => BaseMp + MpPerLevel * (Level - 1);
}