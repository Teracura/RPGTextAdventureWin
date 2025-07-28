using Entities.Heroes;

namespace Entities.Enemies;

public class Goon : IEnemy
{
    public decimal Hp { get; set; }
    public int MaxHp { get; set; }
    public decimal Dmg { get; set; }
    public decimal BaseExp { get; set; }
    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }
    public string Type { get; set; }

    public Goon(decimal ScaleFactor)
    {
        Type = "Goon";
        Hp = 1000 * ScaleFactor;
        MaxHp = (int)Hp;
        Dmg = 100 * ScaleFactor;
        MinLevel = 1;
        MaxLevel = 10;
        BaseExp = 50;
    }

    public bool Attack(IHero hero, decimal ScaleFactor)
    {
        var damage = Dmg - (Dmg * hero.DefencePercentage);
        hero.Hp -= damage;
        return hero.Hp <= 0;
    }
}