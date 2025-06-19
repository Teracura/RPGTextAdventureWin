namespace MainLogic.GlobalParameters;

public class GameStateParameters
{
    public static GameStateParameters Instance { get; } = new();
    public bool Saving { get; set; }
    public MetaProgressionState MetaProgressionState { get; } = new();
    public HeroState HeroState { get; } = new();
    public DungeonState DungeonState { get; } = new();
}