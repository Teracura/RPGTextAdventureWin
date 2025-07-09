using Entities.Enemies;
using Entities.Heroes;
using MainLogic.Factories;
using MainLogic.GlobalParameters;

namespace MainLogic.GameLogic;

public static class GameManager
{
    //This will never be null as Start() is always invoked upon starting the app
#pragma warning disable CS8618
    private static EnemyCreator EnemyCreator { get; set; }
    private static CombatManager CombatManager { get; set; }
    private static Queue<IEnemy> Enemies { get; set; }
#pragma warning restore CS8618
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;


    public static void Start()
    {
        Enemies = new();
        EnemyCreator = new();
        CombatManager = new();
    }

    public static void InitiateFight()
    {
        Instance.DungeonState.NumberOfEnemiesDefeated = 0;

        Enemies.Clear();
        for (int i = 1; i <= Instance.DungeonState.NumberOfEnemiesPerDungeon; i++)
        {
            var enemy = EnemyCreator.CreateNewEnemy(Instance.HeroState.Hero.Level);
            Enemies.Enqueue(enemy);
        }

        if (Enemies.Count > 0)
        {
            CombatManager.CurrentEnemy = Enemies.Dequeue();
            EventManager.SendEnemyAppearMessage(CombatManager.CurrentEnemy);
        }
        else
        {
            EventManager.EnemiesNotFoundMessage();
        }
    }

    public static void RestoreStats()
    {
        var hero = Instance.HeroState.Hero;
        hero.RestoreHealth();
        hero.RestoreMp();
    }

    public static void Attack()
    {
        CombatManager.Attack();
        if (Instance.HeroState.IsDefeated)
        {
            Instance.MetaProgressionState.GlobalTimesDefeated++;
            ShopManager.GetRandomShopItems(Instance.HeroState.Hero.Type!);
            return;
        }

        ReplaceEnemyIfDefeated();
    }

    private static void ReplaceEnemyIfDefeated()
    {
        if (!Instance.DungeonState.EnemyDefeated) return;
        Instance.DungeonState.EnemyDefeated = false;
        Instance.DungeonState.NumberOfEnemiesDefeated++;
        Instance.MetaProgressionState.GlobalEnemiesKilled++;
        Instance.MetaProgressionState.ScaleFactor += 0.001m;
        GainHeroExp();
        if (HasClearedDungeon())
        {
            Instance.DungeonState.DungeonCleared = true;
            Instance.MetaProgressionState.GlobalDungeonsCleared++;
            ShopManager.GetRandomShopItems(Instance.HeroState.Hero.Type!);
            return;
        }

        if (Enemies.Count == 0) return;
        CombatManager.CurrentEnemy = Enemies.Dequeue();
        EventManager.SendEnemyAppearMessage(CombatManager.CurrentEnemy);
    }

    private static void GainHeroExp()
    {
        var currentXp = Instance.HeroState.Hero.Xp;
        var leveledUp = HeroUtils.GainWinningRewards(Instance.HeroState.Hero,
            CombatManager.CurrentEnemy,
            Instance.MetaProgressionState.ScaleFactor);
        HeroUtils.LevelUp(Instance.HeroState.Hero);
        EventManager.SendHeroGainedXpMessage(Instance.HeroState.Hero.Xp - currentXp);
        if (!leveledUp) return;
        EventManager.SendHeroLevelUpMessage(Instance.HeroState.Hero.Level);
    }

    private static bool HasClearedDungeon()
    {
        return Enemies.Count == 0 &&
               Instance.DungeonState.NumberOfEnemiesDefeated >= Instance.DungeonState.NumberOfEnemiesPerDungeon;
    }
}