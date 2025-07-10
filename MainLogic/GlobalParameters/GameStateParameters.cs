using MainLogic.GameLogic;

namespace MainLogic.GlobalParameters;

public class GameStateParameters
{
    public static GameStateParameters Instance { get; } = new();
    public bool Saving { get; set; }
    public MetaProgressionState MetaProgressionState { get; } = new();
    public HeroState HeroState { get; } = new();
    public DungeonState DungeonState { get; } = new();
    public OwnedItemsList OwnedItemsList { get; } = new();
    public GameManager GameManager { get; set; } = null!; //do not put into database or POCO class, will be created upon startup
}