using MainLogic.GlobalParameters;
using Microsoft.EntityFrameworkCore;

namespace MainLogic.AppDataManip;

public class GameDataManager(AppDataDbContext dbContext, ObjectMapper objectMapper)
{
    private static readonly GameStateParameters GameStateInstance = GameStateParameters.Instance;

    public async Task<bool> SaveGameIntoFileSlot(int saveSlot)
    {
        await EnsureDatabaseCreatedAsync();

        var heroBase = objectMapper.ConvertHeroStats(GameStateInstance.HeroState.Hero, saveSlot);
        var gameStateBase = objectMapper.ConvertGameStateParameters(saveSlot);

        var existingHero = await dbContext.Heroes.FindAsync(saveSlot);
        var existingState = await dbContext.GameStateParameters.FindAsync(saveSlot);

        // Full overwrite: remove existing then add new
        if (existingHero != null)
        {
            dbContext.Heroes.Remove(existingHero);
        }

        dbContext.Heroes.Add(heroBase);

        if (existingState != null)
        {
            dbContext.GameStateParameters.Remove(existingState);
        }

        dbContext.GameStateParameters.Add(gameStateBase);

        await dbContext.SaveChangesAsync();
        return true;
    }


    public async Task<bool> LoadGameFromFileSlot(int saveSlot)
    {
        await EnsureDatabaseCreatedAsync();
        var heroBase = await dbContext.Heroes.FindAsync(saveSlot);
        var gameStateBase = await dbContext.GameStateParameters.FindAsync(saveSlot);
        if (heroBase == null || gameStateBase == null) return false;
        var hero = objectMapper.ConvertHeroStats(heroBase);
        objectMapper.ConvertGameStateParameters(gameStateBase);
        GameStateInstance.HeroState.Hero = hero;
        return true;
    }

    public async Task<bool> SaveSlotExistsAsync(int slot)
    {
        var hero = await dbContext.Heroes.FindAsync(slot);
        var gameState = await dbContext.GameStateParameters.FindAsync(slot);
        return hero != null && gameState != null;
    }

    public bool DoesDatabaseExist()
    {
        return dbContext.Database.CanConnect();
    }

    public void EnsureDatabaseCreated()
    {
        dbContext.Database.EnsureCreated();
    }

    public async Task<bool> DoesDatabaseExistAsync()
    {
        return await dbContext.Database.CanConnectAsync();
    }

    public async Task EnsureDatabaseCreatedAsync()
    {
        await dbContext.Database.EnsureCreatedAsync();
    }
}