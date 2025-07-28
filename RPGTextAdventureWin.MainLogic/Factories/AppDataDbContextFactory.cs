using MainLogic.AppDataManip;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MainLogic.Factories;

/// <summary>
/// This factory is only used by the EF Core CLI (dotnet ef) for operations like migrations.
/// </summary>
public class AppDataDbContextFactory : IDesignTimeDbContextFactory<AppDataDbContext>
{
    public AppDataDbContext CreateDbContext(string[] args)
    {
        // Design-time fallback path
        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "DevOnly_SavingAndLoadingData.db");

        var optionsBuilder = new DbContextOptionsBuilder<AppDataDbContext>();
        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new AppDataDbContext(dbPath);
    }
}