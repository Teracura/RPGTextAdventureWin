using MainLogic.AppDataManip;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace MainLogic.Factories;

public class AppDataDbContextFactory : IDesignTimeDbContextFactory<AppDataDbContext>
{
    public AppDataDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDataDbContext>();
        optionsBuilder.UseSqlite("Data Source=gameData.db")
            .LogTo(Console.WriteLine, LogLevel.Information);
        ;
        return new AppDataDbContext(optionsBuilder.Options);
    }
}