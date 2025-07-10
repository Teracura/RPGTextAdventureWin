using Entities.Enemies;
using MainLogic.GlobalParameters;

namespace MainLogic.GameLogic;

public class CombatManager()
{
    public static IEnemy CurrentEnemy { get; set; }
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;


    internal void Attack()
    {
        var hero = Instance.HeroState.Hero;
        hero.Attack(CurrentEnemy);
        if (CurrentEnemy!.Hp <= 0)
        {
            GameStateParameters.Instance.DungeonState.EnemyDefeated = true;
        }

        if (!GameStateParameters.Instance.DungeonState.EnemyDefeated)
        {
            EnemyRetaliation();
        }
        EventManager.SendAttackMessage(CurrentEnemy);
    }

    private void EnemyRetaliation()
    {
        var hero = GameStateParameters.Instance.HeroState.Hero;
        CurrentEnemy?.Attack(hero, GameStateParameters.Instance.MetaProgressionState.ScaleFactor);
        if (hero.Hp > 0) return;
        GameStateParameters.Instance.HeroState.IsDefeated = true;
    }
    
    public void ApplyEnemyDefeatResults()
    {
        Instance.DungeonState.EnemyDefeated = false;
        Instance.DungeonState.NumberOfEnemiesDefeated++;
        Instance.MetaProgressionState.GlobalEnemiesKilled++;
        Instance.MetaProgressionState.ScaleFactor += 0.001m;
    }

    public void ApplyDungeonClearResults()
    {
        Instance.DungeonState.DungeonCleared = true;
        Instance.MetaProgressionState.GlobalDungeonsCleared++;
    }
}