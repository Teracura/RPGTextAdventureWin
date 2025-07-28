using Entities.Heroes;

namespace Entities.Enemies;

// suggested by Youssef Atef, don't blame me if you get obliterated by that duck
public class Duck : IEnemy
{
    public decimal Hp { get; set; }
    public int MaxHp { get; set; }
    public decimal Dmg { get; set; }
    public decimal BaseExp { get; set; }
    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }
    public string Type { get; set; }

    public Duck(decimal scaleFactor)
    {
        Type = "Duck";
        Hp = 100_000_000 * scaleFactor;
        MaxHp = (int)Hp;
        Dmg = 1_000_000 * scaleFactor;
        MinLevel = 100;
        MaxLevel = int.MaxValue;
        BaseExp = 10_000;
    }

    public bool Attack(IHero hero, decimal scaleFactor)
    {
        var damage = Dmg - (Dmg * hero.DefencePercentage);
        hero.Hp -= damage;
        return hero.Hp <= 0;
    }
}