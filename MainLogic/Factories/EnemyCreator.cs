using Entities;
using Entities.Enemies;
using MainLogic.GlobalParameters;

namespace MainLogic.Factories;

public class EnemyCreator
{
    private readonly Dictionary<EnemyType, Func<IEnemy>> _enemyFactories;
    private readonly Random _random = new();
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;

    public EnemyCreator()
    {
        _enemyFactories = new Dictionary<EnemyType, Func<IEnemy>>
        {
            { EnemyType.Goon, () => new Goon(Instance.MetaProgressionState.ScaleFactor) },
            { EnemyType.Junga, () => new Junga(Instance.MetaProgressionState.ScaleFactor) },
            { EnemyType.HomboCombo, () => new HomboCombo(Instance.MetaProgressionState.ScaleFactor) },
            { EnemyType.Dragon, () => new Dragon(Instance.MetaProgressionState.ScaleFactor) },
            { EnemyType.Duck, () => new Duck(Instance.MetaProgressionState.ScaleFactor) }
        };
    }

    public IEnemy CreateNewEnemy(int heroLevel)
    {
        var validEnemyTypes = _enemyFactories.Keys
            .Where(type =>
            {
                var tempEnemy = _enemyFactories[type]();
                return CheckLevel(tempEnemy, heroLevel);
            })
            .ToList();

        if (validEnemyTypes.Count == 0)
            throw new InvalidOperationException($"No enemies available for hero level {heroLevel}");

        var selectedType = validEnemyTypes[_random.Next(validEnemyTypes.Count)];
        return _enemyFactories[selectedType]();
    }


    private bool CheckLevel(IEnemy enemy, int heroLevel)
    {
        return enemy.MinLevel <= heroLevel && enemy.MaxLevel > heroLevel;
    }
}