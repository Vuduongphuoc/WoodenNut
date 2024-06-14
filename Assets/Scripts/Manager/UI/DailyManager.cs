using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DailyManager : MonoBehaviour
{
    public static DailyManager instance;
    [SerializeField] private Text status;
    [SerializeField] private Button claimBtn;

    [Space(4)]
    [SerializeField] private RewardRef smallRewardRef;
    [SerializeField] private Transform bigGiftPos;
    [SerializeField] private RewardRef bigRewardRef;
    [SerializeField] private Transform rewardContainerGrid;

    [Space(6)]
    [SerializeField] private List<Reward> rewards;
    [SerializeField] private List<Reward> bigReward;
    private RewardRef bigRewardPref;
    private List<RewardRef> rewardPrefabs;

    private int currentStreak
    {
        get => PlayerPrefs.GetInt("currentStreak", 0);
        set => PlayerPrefs.SetInt("currentStreak", value);
    }
    private DateTime? lastClaimedTime
    {
        get
        {
            string data = PlayerPrefs.GetString("lastClaimedTime", null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
            {
                PlayerPrefs.SetString("lastClaimedTime", value.ToString());
            }
            else
            {
                PlayerPrefs.DeleteKey("lastClaimedTime");
            }
        }
    }
    private bool canClaimReward;
    private int maxStreakCounter = 6;
    private float claimCD = 24f; // -> CD is CoolDown
    private float claimDL = 48f; // -> DL is DeadLine
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        InitPrefabs();
        StartCoroutine(RewardStateUpdate());
    }
    private void InitPrefabs()
    {
        rewardPrefabs = new List<RewardRef>();
        for (int i = 0; i < maxStreakCounter; i++)
        {
            rewardPrefabs.Add(Instantiate(smallRewardRef, rewardContainerGrid, false));
        }
        bigRewardPref = Instantiate(bigRewardRef, bigGiftPos, false);

    }
    private IEnumerator RewardStateUpdate()
    {
        UpdateRewardsState();
        yield return new WaitForSeconds(1f);
    }
    private void UpdateRewardsState()
    {
        canClaimReward = true;
        if (lastClaimedTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - lastClaimedTime.Value;

            if (timeSpan.TotalHours > claimDL)
            {
                lastClaimedTime = null;
                currentStreak = 0;
            }
            else if (timeSpan.TotalHours < claimCD)
            {
                canClaimReward = false;

            }
            if (currentStreak != 0)
            {
                for (int i = 1; i <= currentStreak; i++)
                {
                    rewards[currentStreak - i].isClaimed = true;
                    rewardPrefabs[currentStreak - i].claimedReward.SetActive(true);
                }
            }
        }
        UpdateRewardsUI();
    }

    private void UpdateRewardsUI()
    {
        claimBtn.interactable = canClaimReward;
        if (canClaimReward)
        {
            status.text = "Claim your reward!";
        }
        else
        {
            var nextClaimTime = lastClaimedTime.Value.AddHours(claimCD);
            var currentClaimCD = nextClaimTime - DateTime.UtcNow;

            string cd = $"{currentClaimCD.Hours:D2}:{currentClaimCD.Minutes:D2}:{currentClaimCD.Seconds:D2}";
            status.text = $"Next daily reward in {cd}";
        }
        for (int i = 0; i < rewardPrefabs.Count; i++)
        {
            rewardPrefabs[i].SetRewardData(i, currentStreak, rewards[i]);
        }
        bigRewardPref.SetBigRewardData(7, bigReward);
    }
    public void ClaimReward()
    {
        if (!canClaimReward)
        {
            return;
        }

        var reward = rewards[currentStreak];
        switch (reward.type)
        {
            case Reward.RewardType.Reverse:
               
                break;
            case Reward.RewardType.RemoveScrew:
                break;
            case Reward.RewardType.Coins:
                UIManager.instance.coin += reward.value;
                break;
        }
        AudioManager.Instance.PlaySFX(AudioManager.Instance.soundEff[6]);
        UIManager.instance.UpdatePlayerUI();
        GameManager.Instance.UpdatePlayerDataFromUI();
        lastClaimedTime = DateTime.UtcNow;
        currentStreak = (currentStreak + 1) % maxStreakCounter;
        
        UpdateRewardsState();
    }
    public void ClaimAdsReward()
    {

    }

}
