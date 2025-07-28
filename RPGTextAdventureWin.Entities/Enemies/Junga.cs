using Entities.Heroes;

namespace Entities.Enemies;

public class Junga : IEnemy
{
    public decimal Hp { get; set; }
    public int MaxHp { get; set; }
    public decimal Dmg { get; set; }
    public decimal BaseExp { get; set; }
    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }
    public string Type { get; set; }

    public Junga(decimal ScaleFactor)
    {
        Type = "Junga";
        Hp = 2000 * ScaleFactor;
        MaxHp = (int)Hp;
        Dmg = 200 * ScaleFactor;
        MinLevel = 5;
        MaxLevel = 20;
        BaseExp = 100;
    }

    public bool Attack(IHero hero, decimal ScaleFactor)
    {
        var damage = Dmg - (Dmg * hero.DefencePercentage);
        hero.Hp -= damage;
        return hero.Hp <= 0;
    }
}