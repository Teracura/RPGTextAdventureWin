using System.ComponentModel.DataAnnotations;

namespace MainLogic.GlobalParameters.BaseEntities;

public class GameStateParametersBaseEntity
{
    [Key]
    public int SaveSlot { get; set; }
    
    //GameStateParameters
    public bool Saving { get; set; }
    
    //HeroState
    public bool IsDefeated { get; set; }
    
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