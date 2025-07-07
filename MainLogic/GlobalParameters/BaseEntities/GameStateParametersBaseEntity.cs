using System.ComponentModel.DataAnnotations;
using Entities.Items;

namespace MainLogic.GlobalParameters.BaseEntities;

public class GameStateParametersBaseEntity
{
    public GameStateParametersBaseEntity()
    {
    }

    public GameStateParametersBaseEntity(int saveSlot,
        bool saving,
        bool isDefeated,
        ItemTypes equippedWeapon,
        ItemTypes equippedArmor,
        ItemTypes equippedAccessory,
        decimal scaleFactor,
        int globalEnemiesKilled,
        int globalTimesDefeated,
        int globalDungeonsCleared,
        bool kaitoKeyEffect,
        int numberOfEnemiesPerDungeon,
        int numberOfEnemiesDefeated,
        bool enemyDefeated,
        bool dungeonCleared)
    {
        SaveSlot = saveSlot;
        Saving = saving;
        IsDefeated = isDefeated;
        EquippedWeapon = equippedWeapon;
        EquippedArmor = equippedArmor;
        EquippedAccessory = equippedAccessory;
        ScaleFactor = scaleFactor;
        GlobalEnemiesKilled = globalEnemiesKilled;
        GlobalTimesDefeated = globalTimesDefeated;
        GlobalDungeonsCleared = globalDungeonsCleared;
        KaitoKeyEffect = kaitoKeyEffect;
        NumberOfEnemiesPerDungeon = numberOfEnemiesPerDungeon;
        NumberOfEnemiesDefeated = numberOfEnemiesDefeated;
        EnemyDefeated = enemyDefeated;
        DungeonCleared = dungeonCleared;
    }

    [Key]
    public int SaveSlot { get; set; }

    //GameStateParameters
    public bool Saving { get; set; }

    //HeroState
    public bool IsDefeated { get; set; }
    public ItemTypes EquippedWeapon { get; set; }
    public ItemTypes EquippedArmor { get; set; }
    public ItemTypes EquippedAccessory { get; set; }

    //MetaProgressionState
    public decimal ScaleFactor { get; set; }
    public int GlobalEnemiesKilled { get; set; }
    public int GlobalTimesDefeated { get; set; }
    public int GlobalDungeonsCleared { get; set; }
    public bool KaitoKeyEffect { get; set; }

    //DungeonState
    public int NumberOfEnemiesPerDungeon { get; set; }
    public int NumberOfEnemiesDefeated { get; set; }
    public bool EnemyDefeated { get; set; }
    public bool DungeonCleared { get; set; }
}