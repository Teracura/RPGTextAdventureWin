using Entities.Items;

namespace MainLogic.GlobalParameters;

public class OwnedItemsList
{
    public Dictionary<ItemTypes, int> Items = Enum.GetValues<ItemTypes>()
        .ToDictionary(itemType => itemType, itemCount => 0);
}