

public class Item
{
    public ItemData itemData;
    public int amount;

    public Item(ItemData data, int amount = 1)
    {
        itemData = data;
        this.amount = amount;
    }
}