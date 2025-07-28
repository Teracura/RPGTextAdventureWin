using Entities.Enemies;
using Entities.Heroes;
using MainLogic.Factories;
using MainLogic.GameLogic.CombatLogic;
using MainLogic.GlobalParameters;

namespace MainLogic.GameLogic;

public class GameManager(
    EnemyCreator enemyCreator,
    CombatManager combatManager,
    Queue<IEnemy> enemies)
{
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;

    public void InitiateFight()
    {
        Instance.DungeonState.NumberOfEnemiesDefeated = 0;
        GenerateDungeonEnemies();

        if (enemies.Count > 0)
        {
            CombatManager.CurrentEnemy = enemies.Dequeue();
            EventManager.SendEnemyAppearMessage(CombatManager.CurrentEnemy);
        }
        else
        {
            EventManager.EnemiesNotFoundMessage();
        }
    }

    public void RestoreStats()
    {
        var hero = Instance.HeroState.Hero;
        hero.RestoreHealth();
        hero.RestoreMp();
    }

    public void Attack()
    {
        combatManager.Attack();
        if (Instance.HeroState.IsDefeated)
        {
            Instance.MetaProgressionState.GlobalTimesDefeated++;
            ShopManager.GetRandomShopItems(Instance.HeroState.Hero.Type!);
            return;
        }

        ReplaceEnemyIfDefeated();
    }

    private void ReplaceEnemyIfDefeated()
    {
        if (!Instance.DungeonState.EnemyDefeated) return;
        combatManager.ApplyEnemyDefeatResults();
        GainHeroExp();
        if (HasClearedDungeon())
        {
            combatManager.ApplyDungeonClearResults();
            ShopManager.GetRandomShopItems(Instance.HeroState.Hero.Type!);
            return;
        }

        if (enemies.Count == 0) return;
        CombatManager.CurrentEnemy = enemies.Dequeue();
        EventManager.SendEnemyAppearMessage(CombatManager.CurrentEnemy);
    }

    private void GainHeroExp()
    {
        var hero = Instance.HeroState.Hero;
        var currentXp = hero.Xp;
        var leveledUp = HeroUtils.GainWinningRewards(hero,
            CombatManager.CurrentEnemy,
            Instance.MetaProgressionState.ScaleFactor);
        
        HeroUtils.LevelUp(hero);
        EventManager.SendHeroGainedXpMessage(hero.Xp - currentXp);
        if (!leveledUp) return;
        EventManager.SendHeroLevelUpMessage(hero.Level);
    }

    private bool HasClearedDungeon()
    {
        return enemies.Count == 0 &&
               Instance.DungeonState.NumberOfEnemiesDefeated >= Instance.DungeonState.NumberOfEnemiesPerDungeon;
    }

    private void GenerateDungeonEnemies()
    {
        enemies.Clear();
        for (var i = 1; i <= Instance.DungeonState.NumberOfEnemiesPerDungeon; i++)
        {
            var enemy = enemyCreator.CreateNewEnemy(Instance.HeroState.Hero.Level);
            enemies.Enqueue(enemy);
        }
    }

    public bool UseSpecialAttack(ISpecialAttack attack)
    {
        return combatManager.UseSpecialAbility(attack);
    }
}