using Entities.Heroes;

namespace MainLogic.GlobalParameters;

public class HeroState
{
    public IHero Hero { get; set; } = null!;
    public bool IsDefeated { get; set; }
}