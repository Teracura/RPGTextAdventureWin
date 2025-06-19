using Entities.Heroes;

namespace Entities.Enemies;

public class HomboCombo : IEnemy
{
    public decimal Hp { get; set; }
    public int MaxHp { get; set; }
    public decimal Dmg { get; set; }
    public decimal BaseExp { get; set; }
    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }
    public string Type { get; set; }

    public HomboCombo(decimal ScaleFactor)
    {
        Type = "HomboCombo";
        Hp = 4000 * ScaleFactor;
        MaxHp = (int)Hp;
        Dmg = 300 * ScaleFactor;
        MinLevel = 15;
        MaxLevel = 30;
        BaseExp = 200;
    }

    public bool Attack(IHero hero, decimal scaleFactor)
    {
        decimal damage;
        if ((1000 * scaleFactor / 2) <= Hp)
        {
            damage = Dmg - (Dmg * hero.DefencePercentage);
        }
        else
        {
            damage = Dmg * 1.5m - (Dmg * 1.5m * hero.DefencePercentage);
        }
        hero.Hp -= damage;
        return hero.Hp <= 0;
    }
}