using Entities.Heroes;
using Entities.Items;
using MainLogic.GlobalParameters.BaseEntities;
using Microsoft.EntityFrameworkCore;

namespace MainLogic.AppDataManip;

public class AppDataDbContext() : DbContext
{
    public DbSet<GameStateParametersBaseEntity> GameStateParameters { get; set; }
    public DbSet<HeroBaseEntity> Heroes { get; set; }
    public DbSet<OwnedItemsBase> OwnedItems { get; set; }
    public DbSet<Item> ShopItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        
        optionsBuilder.UseSqlite($"Data Source=SavingAndLoadingData.db");
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

        modelBuilder.Entity<OwnedItemsBase>()
            .HasKey(i => i.SaveSlot);
        modelBuilder.Entity<OwnedItemsBase>()
            .Property(e => e.SaveSlot).ValueGeneratedNever();

        modelBuilder.Entity<Item>()
            .HasKey(i => i.ItemId);
    }
}