using Entities.Heroes;
using MainLogic.GlobalParameters;
using MainLogic.GlobalParameters.BaseEntities;

namespace MainLogic;

public class ObjectMapper
{
    public HeroBaseEntity ConvertHeroStats(IHero hero, int saveId)
    {
        HeroBaseEntity entity = hero switch
        {
            Warrior => new WarriorEntity(),
            Mage => new MageEntity(),
            Archer => new ArcherEntity(),
            _ => throw new InvalidOperationException("Unknown hero type")
        };

        CopyHeroStatsCommon(hero, entity);
        entity.SaveSlot = saveId;
        return entity;
    }

    public IHero ConvertHeroStats(HeroBaseEntity hero)
    {
        IHero domainHero = hero switch
        {
            WarriorEntity => new Warrior(),
            MageEntity => new Mage(),
            ArcherEntity => new Archer(),
            _ => throw new InvalidOperationException("Unknown hero entity type")
        };

        CopyHeroStatsCommon(hero, domainHero);
        return domainHero;
    }

    public GameStateParametersBaseEntity ConvertGameStateParameters(int saveId)
    {
        var state = GameStateParameters.Instance;
        var stateBase = new GameStateParametersBaseEntity();

        CopyGameStateParametersCommon(state, stateBase);
        stateBase.SaveSlot = saveId;
        return stateBase;
    }

    public void ConvertGameStateParameters(GameStateParametersBaseEntity stateBase)
    {
        var state = GameStateParameters.Instance;

        CopyGameStateParametersCommon(stateBase, state);
    }

    private void CopyGameStateParametersCommon(object source, object target)
    {
        switch ((source, target))
        {
            case (GameStateParameters src, GameStateParametersBaseEntity dest):
                dest.Saving = src.Saving;
                dest.DungeonCleared = src.DungeonState.DungeonCleared;
                dest.GlobalTimesDefeated = src.MetaProgressionState.GlobalTimesDefeated;
                dest.GlobalEnemiesKilled = src.MetaProgressionState.GlobalEnemiesKilled;
                dest.GlobalDungeonsCleared = src.MetaProgressionState.GlobalDungeonsCleared;
                dest.KaitoKeyEffect = src.MetaProgressionState.KaitoKeyEffect;
                dest.ScaleFactor = src.MetaProgressionState.ScaleFactor;
                dest.NumberOfEnemiesDefeated = src.DungeonState.NumberOfEnemiesDefeated;
                dest.NumberOfEnemiesPerDungeon = src.DungeonState.NumberOfEnemiesPerDungeon;
                dest.IsDefeated = src.HeroState.IsDefeated;
                dest.EnemyDefeated = src.DungeonState.EnemyDefeated;
                break;
            case (GameStateParametersBaseEntity src, GameStateParameters dest):
                dest.Saving = src.Saving;
                dest.DungeonState.DungeonCleared = src.DungeonCleared;
                dest.MetaProgressionState.GlobalTimesDefeated = src.GlobalTimesDefeated;
                dest.MetaProgressionState.GlobalEnemiesKilled = src.GlobalEnemiesKilled;
                dest.MetaProgressionState.GlobalDungeonsCleared = src.GlobalDungeonsCleared;
                dest.MetaProgressionState.KaitoKeyEffect = src.KaitoKeyEffect;
                dest.MetaProgressionState.ScaleFactor = src.ScaleFactor;
                dest.DungeonState.NumberOfEnemiesDefeated = src.NumberOfEnemiesDefeated;
                dest.DungeonState.NumberOfEnemiesPerDungeon = src.NumberOfEnemiesPerDungeon;
                dest.HeroState.IsDefeated = src.IsDefeated;
                dest.DungeonState.EnemyDefeated = src.EnemyDefeated;
                break;
            default:
                throw new InvalidOperationException("Unsupported mapping types.");
        }
    }

    private void CopyHeroStatsCommon(object source, object target)
    {
        switch ((source, target))
        {
            case (IHero src, HeroBaseEntity dest):
                dest.Type = src.Type;
                dest.Hp = src.Hp;
                dest.Dmg = src.Dmg;
                dest.Mp = src.Mp;
                dest.DefencePercentage = src.DefencePercentage;
                dest.MaxDefense = src.MaxDefense;
                dest.Xp = src.Xp;
                dest.Level = src.Level;
                dest.TurnCounter = src.TurnCounter;
                dest.Money = src.Money;
                dest.DefenseIncreasePerLevel = src.DefenseIncreasePerLevel;
                dest.DmgPerLevel = src.DmgPerLevel;
                dest.HpPerLevel = src.HpPerLevel;
                break;

            case (HeroBaseEntity src, IHero dest):
                dest.Type = src.Type;
                dest.Hp = src.Hp;
                dest.Dmg = src.Dmg;
                dest.Mp = src.Mp;
                dest.DefencePercentage = src.DefencePercentage;
                dest.MaxDefense = src.MaxDefense;
                dest.Xp = src.Xp;
                dest.Level = src.Level;
                dest.TurnCounter = src.TurnCounter;
                dest.Money = src.Money;
                dest.DefenseIncreasePerLevel = src.DefenseIncreasePerLevel;
                dest.DmgPerLevel = src.DmgPerLevel;
                dest.HpPerLevel = src.HpPerLevel;
                break;

            default:
                throw new InvalidOperationException("Unsupported mapping types.");
        }
    }
    
}