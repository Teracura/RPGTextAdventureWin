using System.Diagnostics;
using Entities.Heroes;
using Entities.Items;
using MainLogic.GameLogic;
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
        var ownedItemsBase = objectMapper.ConvertDictionaryStats(saveSlot); //bug is here

        var existingHero = await dbContext.Heroes.FindAsync(saveSlot);
        var existingGameState = await dbContext.GameStateParameters.FindAsync(saveSlot);
        var existingOwnedItems = await dbContext.OwnedItems.FindAsync(saveSlot);
        SaveShopListing(saveSlot);
        if (existingHero != null)
        {
            dbContext.Heroes.Remove(existingHero);
        }
        dbContext.Heroes.Add(heroBase);
        
        if (existingGameState != null)
        {
            dbContext.Remove(existingGameState);
        }
        dbContext.GameStateParameters.Add(gameStateBase);

        if (existingOwnedItems != null)
        {
            dbContext.OwnedItems.Remove(existingOwnedItems);
        }
        dbContext.OwnedItems.Add(ownedItemsBase);

        await dbContext.SaveChangesAsync();
        return true;
    }

    private void SaveShopListing(int saveSlot)
    {
        try
        {
            var oldItems = dbContext.ShopItems.Where(i => i.SaveSlot == saveSlot);
            dbContext.ShopItems.RemoveRange(oldItems);
        }
        catch (Exception e)
        {
            // ignored
        }
        finally
        {
            var shopItemsToSave = ShopManager.ShopItems.Select(item => new Item
            {
                Type = item.Type,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                SaveSlot = saveSlot
            });
            dbContext.ShopItems.AddRange(shopItemsToSave);
        }
    }


    public async Task<bool> LoadGameFromFileSlot(int saveSlot)
    {
        await EnsureDatabaseCreatedAsync();
        var heroBase = await dbContext.Heroes.FindAsync(saveSlot);
        var gameStateBase = await dbContext.GameStateParameters.FindAsync(saveSlot);
        var ownedItemsBase = await dbContext.OwnedItems
            .Where(x => x.SaveSlot == saveSlot)
            .FirstOrDefaultAsync();
        LoadShopListing(saveSlot);

        if (heroBase == null || gameStateBase == null || ownedItemsBase == null) return false;
        var hero = objectMapper.ConvertHeroStats(heroBase);
        objectMapper.ConvertGameStateParameters(gameStateBase);
        objectMapper.ConvertDictionaryStats(ownedItemsBase);
        GameStateInstance.HeroState.Hero = hero;
        return true;
    }

    private void LoadShopListing(int saveSlot)
    {
        var items = dbContext.ShopItems
            .Where(i => i.SaveSlot == saveSlot)
            .ToList();

        ShopManager.ShopItems = items;
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