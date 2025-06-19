using Entities.Heroes;
using MainLogic.GlobalParameters.BaseEntities;
using Microsoft.EntityFrameworkCore;

namespace MainLogic.AppDataManip;

public class AppDataDbContext(DbContextOptions<AppDataDbContext> optionsBuilderOptions) : DbContext
{
    public DbSet<GameStateParametersBaseEntity> GameStateParameters { get; set; }
    public DbSet<HeroBaseEntity> Heroes { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=gameData.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GameStateParametersBaseEntity>()
            .HasKey(e => e.SaveSlot);

        modelBuilder.Entity<GameStateParametersBaseEntity>()
            .Property(e => e.SaveSlot).ValueGeneratedNever();

        modelBuilder.Entity<HeroBaseEntity>()
            .HasDiscriminator<string>("HeroType")
            .HasValue<WarriorEntity>("Warrior")
            .HasValue<MageEntity>("Mage")
            .HasValue<ArcherEntity>("Archer");

        modelBuilder.Entity<HeroBaseEntity>()
            .HasKey(h => h.SaveSlot);

        modelBuilder.Entity<HeroBaseEntity>()
            .Property(e => e.SaveSlot).ValueGeneratedNever();
    }
}