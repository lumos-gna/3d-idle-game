

public class Item
{
    public ItemData itemData;
    public int count;

    public Item(ItemData data, int count = 1)
    {
        itemData = data;
        this.count = count;
    }
}