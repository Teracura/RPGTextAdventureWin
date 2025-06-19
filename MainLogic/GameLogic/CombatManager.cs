using Entities.Enemies;
using MainLogic.GlobalParameters;

namespace MainLogic.GameLogic;

public class CombatManager()
{
    public static IEnemy? CurrentEnemy { get; set; }

    public static event Action<string>? OnHeroAttackMessage;
    public static event Action<string>? OnEnemyAttackMessage;
    public static event Action<string>? OnHealMessage; //Will be used later

    internal void Attack()
    {
        var hero = GameStateParameters.Instance.HeroState.Hero;
        hero.Attack(CurrentEnemy);
        OnHeroAttackMessage?.Invoke(
            $"You attacked {CurrentEnemy?.Type} for {hero.CalculateActualDamage()} damage!");
        if (CurrentEnemy!.Hp <= 0)
        {
            OnHeroAttackMessage?.Invoke($"{CurrentEnemy.Type} has been defeated!");
            GameStateParameters.Instance.DungeonState.EnemyDefeated = true;
        }

        if (GameStateParameters.Instance.DungeonState.EnemyDefeated) return;
        EnemyRetaliation();
    }

    private void EnemyRetaliation()
    {
        var hero = GameStateParameters.Instance.HeroState.Hero;
        OnEnemyAttackMessage?.Invoke(
            $"{CurrentEnemy?.Type} retaliated for {CurrentEnemy?.Dmg - CurrentEnemy?.Dmg * hero.DefencePercentage} damage!");
        CurrentEnemy?.Attack(hero, GameStateParameters.Instance.MetaProgressionState.ScaleFactor);
        if (hero.Hp > 0) return;
        GameStateParameters.Instance.HeroState.IsDefeated = true;
        OnEnemyAttackMessage?.Invoke("You have been hit fatally!");
    }
}