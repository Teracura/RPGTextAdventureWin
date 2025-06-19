namespace Entities.Items;

public class Item(ItemTypes type, string name, string description, int price)
{
    public ItemTypes Type { get; } = type;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public int Price { get; } = price;

    public bool CanBeUsedBy(string heroType)
    {
        var itemName = Type.ToString();
        return itemName.StartsWith("Common_") || itemName.StartsWith($"{heroType}_");
    }
}