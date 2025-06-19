using Entities.Enemies;
using Entities.Heroes;
using MainLogic.Factories;
using MainLogic.GlobalParameters;

namespace MainLogic.GameLogic;

public class GameManager
{
    private static EnemyCreator? EnemyCreator { get; set; }
    private static CombatManager? CombatManager { get; set; }
    public static Queue<IEnemy?>? Enemies { get; set; }

    // Events for general game flow messages (e.g., "Battle Initiated!")
    public static event Action<string>? OnGameMessage;

    // Events for specific combat messages (damage dealt, healing, etc.)
    public static event Action<string>? OnHeroCombatMessage;

    public static event Action<string>? OnEnemyCombatMessage;

    // You might also want an event for when the game ends
    public static event Action<string>? OnGameEnded; // e.g., "Victory!" or "Game Over!"


    public static void Start()
    {
        Enemies = new();
        EnemyCreator = new();
        CombatManager = new();
    }

    public static void InitiateFight()
    {
        GameStateParameters.Instance.DungeonState.NumberOfEnemiesDefeated = 0;
        Enemies?.Clear();
        CombatManager.CurrentEnemy = null;
        for (int i = 1; i <= GameStateParameters.Instance.DungeonState.NumberOfEnemiesPerDungeon; i++)
        {
            var enemy = EnemyCreator?.CreateNewEnemy(GameStateParameters.Instance.HeroState.Hero.Level);
            Enemies?.Enqueue(enemy);
        }

        if (Enemies != null && Enemies.Count != 0)
        {
            CombatManager.CurrentEnemy ??= Enemies.Dequeue();
            OnGameMessage?.Invoke("Battle has begun! A wild " + CombatManager.CurrentEnemy?.Type +
                                  " appeared!"); // Raise initial message
        }
        else
        {
            OnGameMessage?.Invoke("No enemies found for this fight.");
        }
    }

    public static void RestoreStats()
    {
        var hero = GameStateParameters.Instance.HeroState.Hero;
        hero.RestoreHealth();
        hero.RestoreMp();
    }

    public static void Attack()
    {
        CombatManager?.Attack();
        if (GameStateParameters.Instance.HeroState.IsDefeated)
        {
            GameStateParameters.Instance.MetaProgressionState.GlobalTimesDefeated++;
            OnGameEnded?.Invoke("Game Over! Your hero has been defeated.");
            return;
        }

        ReplaceEnemyIfDefeated();
    }

    private static void ReplaceEnemyIfDefeated()
    {
        if (!GameStateParameters.Instance.DungeonState.EnemyDefeated) return;
        GameStateParameters.Instance.DungeonState.EnemyDefeated = false;
        GameStateParameters.Instance.DungeonState.NumberOfEnemiesDefeated++;
        GameStateParameters.Instance.MetaProgressionState.GlobalEnemiesKilled++;
        GameStateParameters.Instance.MetaProgressionState.ScaleFactor += 0.001m;
        GainHeroExp();
        if (VictoryIfAllEnemiesDefeated())
        {
            OnGameEnded?.Invoke("Victory! All enemies defeated!");
            GameStateParameters.Instance.DungeonState.DungeonCleared = true;
            GameStateParameters.Instance.MetaProgressionState.GlobalDungeonsCleared++;
            return;
        }

        if (Enemies == null || Enemies.Count == 0) return;
        CombatManager.CurrentEnemy = Enemies.Dequeue();
        OnGameMessage?.Invoke($"A new enemy appeared: {CombatManager.CurrentEnemy?.Type}!");
    }

    private static void GainHeroExp()
    {
        var currentXp = GameStateParameters.Instance.HeroState.Hero.Xp;
        if (HeroUtils.GainWinningRewards(GameStateParameters.Instance.HeroState.Hero, CombatManager.CurrentEnemy,
                GameStateParameters.Instance.MetaProgressionState.ScaleFactor))
        {
            HeroUtils.LevelUp(GameStateParameters.Instance.HeroState.Hero);
            OnGameMessage?.Invoke(
                $"Gained {GameStateParameters.Instance.HeroState.Hero.Xp - currentXp} XP! and leveled up to level {GameStateParameters.Instance.HeroState.Hero.Level}!");
        }
        else
        {
            OnGameMessage?.Invoke(
                $"Gained {GameStateParameters.Instance.HeroState.Hero.Xp - currentXp} XP!");
        }

        CombatManager.CurrentEnemy = null;
    }

    private static bool VictoryIfAllEnemiesDefeated()
    {
        if (Enemies!.Count == 0 &&
            GameStateParameters.Instance.DungeonState.NumberOfEnemiesDefeated ==
            GameStateParameters.Instance.DungeonState.NumberOfEnemiesPerDungeon) return true;
        //TODO: Implement Victory Screen
        return false;
    }
}