using Entities.Enemies;
using MainLogic.Factories;
using MainLogic.GameLogic.CombatLogic.SpecialAttacks;
using MainLogic.GlobalParameters;

namespace MainLogic.GameLogic.CombatLogic;

public class CombatManager()
{
    public static IEnemy CurrentEnemy { get; set; }
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;

    internal bool UseSpecialAbility(ISpecialAttack attack)
    {
        var hero = Instance.HeroState.Hero;
        if (attack.RealCooldown > 0) return false;
        attack.Apply(hero);
        EventManager.SendSpecialAttackUseMessage(attack, CurrentEnemy);
        RetaliateIfNotDefeated();
        return true;
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