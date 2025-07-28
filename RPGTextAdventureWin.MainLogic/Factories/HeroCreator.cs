using Entities.Heroes;

namespace MainLogic.Factories;

public class HeroCreator
{
    private readonly Dictionary<string, Func<IHero>> _heroFactories = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Warrior", () => new Warrior() },
        { "Archer", () => new Archer() },
        { "Mage", () => new Mage() }
    };

    public IHero CreateNewHero(string type)
    {
        if (!_heroFactories.TryGetValue(type, out var factory))
        {
            throw new ArgumentException($"Unknown hero type: {type}");
        }

        var hero = factory();
        return hero;
    }

}