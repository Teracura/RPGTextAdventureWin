using MainLogic.AppDataManip;

namespace RPGTextAdventureWin;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        using var context = new AppDataDbContext();
        _ = context.Heroes.FirstOrDefault(); //warming up the database for more snappy first-load of save page
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}