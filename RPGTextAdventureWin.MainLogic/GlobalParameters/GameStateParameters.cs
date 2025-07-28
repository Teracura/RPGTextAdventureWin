using Entities.Enemies;
using Entities.Heroes;
using Entities.Items;
using MainLogic.Factories;
using MainLogic.GameLogic;
using MainLogic.GameLogic.CombatLogic;

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
    
    public void ResetState(IHero newHero)
    {
        OwnedItemsList.Items.Clear();
        HeroState.Hero = newHero;
        MetaProgressionState.ScaleFactor = 1m;
        MetaProgressionState.GlobalTimesDefeated = 0;
        MetaProgressionState.GlobalEnemiesKilled = 0;
        MetaProgressionState.GlobalDungeonsCleared = 0;
        HeroState.EquippedAccessory = ItemTypes.Nothing;
        HeroState.EquippedArmor = ItemTypes.Nothing;
        HeroState.EquippedWeapon = ItemTypes.Nothing;
        DungeonState.NumberOfEnemiesPerDungeon = 5;
        ShopManager.GetRandomShopItems(HeroState.Hero.Type!);
        GameManager = new GameManager(new EnemyCreator(), new CombatManager(), new Queue<IEnemy>());
    }

}