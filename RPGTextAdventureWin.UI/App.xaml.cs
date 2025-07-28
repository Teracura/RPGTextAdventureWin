using System.Diagnostics;
using MainLogic.AppDataManip;

namespace RPGTextAdventureWin;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "SavingAndLoadingData.db");
        Debug.WriteLine("Database Path: " + dbPath);
        var context = new AppDataDbContext(dbPath);
        context.Database.EnsureCreated();
        _ = context.Heroes.FirstOrDefault(); //warming up the database for more snappy first-load of save page
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}