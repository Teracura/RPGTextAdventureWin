using Entities.Heroes;

namespace MainLogic.Factories;

public class HeroCreator
{
    private readonly Dictionary<string, Func<IHero>> _heroStringFactories = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Warrior", () => new Warrior() },
        { "Archer", () => new Archer() },
        { "Mage", () => new Mage() }
    };

    private readonly Dictionary<HeroClasses, Func<IHero>> _heroEnumFactories = new()
    {
        { HeroClasses.Warrior, () => new Warrior() },
        { HeroClasses.Archer, () => new Archer() },
        { HeroClasses.Mage, () => new Mage() }
    };
    
    private static IHero CreateHeroFromFactory<T>(Dictionary<T, Func<IHero>> factoryMap, T key) where T : notnull
    {
        if (!factoryMap.TryGetValue(key, out var factory))
        {
            throw new ArgumentException($"Unknown hero type: {key}");
        }

        return factory();
    }

    public IHero CreateNewHero(string type) =>
        CreateHeroFromFactory(_heroStringFactories, type);

    public IHero CreateNewHero(HeroClasses type) =>
        CreateHeroFromFactory(_heroEnumFactories, type);


}