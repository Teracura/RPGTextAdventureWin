using Entities.Heroes;

namespace Entities.Enemies;

public interface IEnemy
{
    decimal Hp { get; set; }
    int MaxHp { get; set; }
    decimal Dmg { get; set; }
    decimal BaseExp { get; set; }
    int MinLevel { get; set; }
    int MaxLevel { get; set; }
    string Type { get; set; }
    
    
    bool Attack(IHero hero, decimal ScaleFactor);
}