using Entities.Enemies;
using MainLogic.Factories;
using MainLogic.GameLogic.CombatLogic.SpecialAttacks;
using MainLogic.GlobalParameters;

namespace MainLogic.GameLogic.CombatLogic;

public class CombatManager()
{
    public static IEnemy CurrentEnemy { get; set; }
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;

    internal void UseSpecialAbility(SpecialAttackTypes specialAttackType)
    {
        var hero = Instance.HeroState.Hero;
        var specialAttack = SpecialAttackFactory.Create(specialAttackType);
        specialAttack.Apply(hero);
        EventManager.SendSpecialAttackUseMessage(specialAttackType, CurrentEnemy);
        RetaliateIfNotDefeated();
    }
    
    internal void Attack()
    {
        var hero = Instance.HeroState.Hero;
        hero.Attack(CurrentEnemy);
        RetaliateIfNotDefeated();
        EventManager.SendAttackMessage(CurrentEnemy);
    }

    private void RetaliateIfNotDefeated()
    {
        if (CurrentEnemy!.Hp <= 0)
        {
            Instance.DungeonState.EnemyDefeated = true;
        }

        if (!Instance.DungeonState.EnemyDefeated)
        {
            EnemyRetaliation();
        }
    }

    private void EnemyRetaliation()
    {
        var hero = Instance.HeroState.Hero;
        CurrentEnemy?.Attack(hero, Instance.MetaProgressionState.ScaleFactor);
        if (hero.Hp > 0) return;
        Instance.HeroState.IsDefeated = true;
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