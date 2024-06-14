[System.Serializable]

public class Item_Shop
{
    public enum ItemType
    {
        Coins_small,
        Coins_medium,
        Coins_large,
        Chest_small,
        Chest_medium,
        Chest_large,
    }
    public ItemType type;
    public int value;

}
