using MainLogic.GlobalParameters;

namespace MainLogic.AppDataManip;

public class GameDataManager(AppDataDbContext dbContext, ObjectMapper objectMapper)
{
    private static readonly GameStateParameters GameStateInstance = GameStateParameters.Instance;

    public async Task SaveGameIntoFileSlot(int saveSlot)
    {
        await EnsureDatabaseCreatedAsync();

        var heroBase = objectMapper.ConvertHeroStats(GameStateInstance.HeroState.Hero, saveSlot);
        var gameStateBase = objectMapper.ConvertGameStateParameters(saveSlot);

        // Check if the entries already exist
        var existingHero = await dbContext.Heroes.FindAsync(saveSlot);
        var existingState = await dbContext.GameStateParameters.FindAsync(saveSlot);

        if (existingHero == null)
            dbContext.Heroes.Add(heroBase);
        else
            dbContext.Heroes.Update(heroBase);

        if (existingState == null)
            dbContext.GameStateParameters.Add(gameStateBase);
        else
            dbContext.GameStateParameters.Update(gameStateBase);

        await dbContext.SaveChangesAsync();
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