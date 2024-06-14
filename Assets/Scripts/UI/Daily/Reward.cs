[System.Serializable]

public class Reward
{
    public enum RewardType
    {
        Coins,
        Reverse,
        RemoveScrew,
        All
    }
    public RewardType type;
    public int value;
    public bool isClaimed;
}