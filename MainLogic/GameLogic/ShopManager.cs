using Entities.Items;

namespace MainLogic.GameLogic;

public class ShopManager
{
    public static IEnumerable<Item> GetRandomShopItems(string heroType, int count = 5)
    {
        var availableItems = ItemCatalog.GetItemsForHero(heroType).ToList();
        var random = new Random();
        var shopItems = new List<Item>();
            
        for (var i = 0; i < count && availableItems.Count > 0; i++)
        {
            var index = random.Next(availableItems.Count);
            shopItems.Add(availableItems[index]);
            availableItems.RemoveAt(index);
        }
            
        return shopItems;
    }
}