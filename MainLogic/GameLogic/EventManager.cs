using Entities.Enemies;
using MainLogic.GameLogic.CombatLogic.SpecialAttacks;
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
        GameMessageNotification?.Invoke($"You gained {xp} xp!");
    }

    public static void SendHeroLevelUpMessage(int level)
    {
        GameMessageNotification?.Invoke($"You leveled up to level {level}!");
    }

    public static void SendSpecialAttackUseMessage(ISpecialAttack attack, IEnemy enemy)
    {
        var hero = Instance.HeroState.Hero;
        var type = attack.GetType();
        switch (type)
        {
            // Warrior
            case SpecialAttackTypes.StrikeOfTheAvenger:
                GameMessageNotification?.Invoke("You used Strike of the Avenger!");
                HeroSideNotification?.Invoke($"You attacked {enemy.Type} for " +
                                             $"{hero.Dmg *
                                                (Math.Max(1, 4 * ((hero.CalculateMaxHp() - (hero.Hp + 1)) / hero.CalculateMaxHp())))} damage!");
                break;
            case SpecialAttackTypes.AbsoluteGuard:
                GameMessageNotification?.Invoke("You used Absolute Guard!");
                HeroSideNotification?.Invoke($"You attacked {enemy.Type} for {hero.Dmg * 1.5m} damage!");
                break;
            case SpecialAttackTypes.UltimateRageStrike:
                break;

            // Mage
            case SpecialAttackTypes.SoulSwap:
                GameMessageNotification?.Invoke("You used Soul Swap!");
                HeroSideNotification?.Invoke("You swapped your mana and HP!");
                break;
            case SpecialAttackTypes.SparkOfTheMightiestMage:
                GameMessageNotification?.Invoke("You used Spark of the Mightiest Mage!");
                HeroSideNotification?.Invoke("Mana is now at 0");
                break;
            case SpecialAttackTypes.SpiritLeech:
                break;

            // Archer
            case SpecialAttackTypes.TheFierceShot:
                break;
            case SpecialAttackTypes.ElvenCursedArrow:
                break;
            case SpecialAttackTypes.FocusShot:
                break;

            default:
                GameMessageNotification?.Invoke("ERROR: SPECIAL ATTACK NOT IMPLEMENTED");
                break;
        }
    }
}