[System.Serializable]

public class ItemPack_Shop
{
    public enum PackType
    {
        Ads,
        Super,
        Mega,
    }
    public PackType type;
    public int value;
}