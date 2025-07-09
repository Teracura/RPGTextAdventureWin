using Entities.Enemies;
using MainLogic.GlobalParameters;

namespace MainLogic.GameLogic;

public static class EventManager
{
    public static event Action<string>? HeroSideNotification;
    public static event Action<string>? EnemySideNotification;
    public static event Action<string>? GameMessageNotification;
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;
    public static void SendEnemyAppearMessage(IEnemy enemy)
    {
        GameMessageNotification?.Invoke("A wild " + enemy.Type +
                              " appeared!");
    }

    public static void EnemiesNotFoundMessage()
    {
        GameMessageNotification?.Invoke("No enemies found for this fight.");
    }
    public static void SendAttackMessage(IEnemy enemy)
    {
        HeroSideNotification?.Invoke(
            $"You attacked {enemy.Type} for {Instance.HeroState.Hero.Dmg} damage!"); //change when you implement enemy special abilities related to defense

        EnemySideNotification?.Invoke(enemy.Hp <= 0
            ? $"{enemy.Type} has been defeated!"
            : $"{enemy.Type} retaliated for {enemy.Dmg - enemy.Dmg * Instance.HeroState.Hero.DefencePercentage} damage!");

        if (Instance.HeroState.Hero.Hp > 0) return;
        HeroSideNotification?.Invoke("You have been hit fatally!");
    }
    
    public static void SendHeroGainedXpMessage(decimal xp)
    {
        HeroSideNotification?.Invoke($"You gained {xp} xp!");
    }
    
    public static void SendHeroLevelUpMessage(int level)
    {
        HeroSideNotification?.Invoke($"You leveled up to level {level}!");
    }
}