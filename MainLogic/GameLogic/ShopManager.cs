using Entities.Items;
using MainLogic.GlobalParameters;

namespace MainLogic.GameLogic;

public static class ShopManager
{
    public static List<Item> ShopItems { get; set; } = [];
    private static readonly GameStateParameters Instance = GameStateParameters.Instance;
    public static void GetRandomShopItems(string heroType, int count = 5)
    {
        var availableItems = ItemCatalog.GetItemsForHero(heroType).ToList();
        var random = new Random();
        ShopItems.Clear();

        for (var i = 0; i < count && availableItems.Count > 0; i++)
        {
            var index = random.Next(availableItems.Count);
            ShopItems.Add(availableItems[index]);
            availableItems.RemoveAt(index);
        }
    }

    public static bool BuyItem(Item item)
    {
        if (!ShopItems.Contains(item))
            return false;

        if (Instance.HeroState.Hero.Money < item.Price)
            return false;

        Instance.HeroState.Hero.Money -= item.Price;
        Instance.OwnedItemsList.Items[item.Type]++;
        ShopItems.Remove(item);

        return true;
    }

    public static void SellItem(int sellId)
    {
        var listOfItems = Instance.OwnedItemsList.Items;
        if (sellId < 1 || sellId >= listOfItems.Count + 1)
            throw new ArgumentOutOfRangeException(nameof(sellId), "Invalid item index.");
        var itemValue = listOfItems.ElementAt(sellId - 1).Key;
        var item = ItemCatalog.GetItemData(itemValue);
        listOfItems[itemValue]--;
        Instance.HeroState.Hero.Money += (int)(item.Price * 0.6);
    }
}