using Entities.Heroes;

namespace Entities.Enemies;

public class Dragon : IEnemy
{
    public decimal Hp { get; set; }
    public int MaxHp { get; set; }
    public decimal Dmg { get; set; }
    public decimal BaseExp { get; set; }
    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }
    public string Type { get; set; }

    public Dragon(decimal ScaleFactor)
    {
        Type = "Dragon";
        Hp = 7000 * ScaleFactor;
        MaxHp = (int)Hp;
        Dmg = 500 * ScaleFactor;
        MinLevel = 25;
        MaxLevel = 100;
        BaseExp = 500;
    }

    public bool Attack(IHero hero, decimal ScaleFactor)    {
        var damage = Dmg - (Dmg * hero.DefencePercentage);
        hero.Hp -= damage;
        return hero.Hp <= 0;
    }
}