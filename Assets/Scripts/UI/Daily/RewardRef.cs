using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardRef : MonoBehaviour
{
    public GameObject claimedReward;

    [Space(6)]
    [SerializeField] private Text dayTxt;
    [SerializeField] private Sprite[] rewardSprs;

    [Space(7)]
    [Header("Small gift")]
    [SerializeField] private Image rewardIcon;  
    [SerializeField] private Text rewardValue;
    [SerializeField] private GameObject outLine;


    [Space(7)]
    [Header("Big gift")]
    [SerializeField] private Image[] rewardIcons;
    [SerializeField] private Text[] rewardsValue; 


    public void SetRewardData(int day, int currentStreak, Reward rewards)
    {
        dayTxt.text = $"Day {day + 1}";
        rewardIcon.sprite = rewards.type == Reward.RewardType.Coins ? rewardSprs[0] :
             rewardIcon.sprite = rewards.type == Reward.RewardType.Reverse? rewardSprs[1] : rewardSprs[2];

        rewardValue.text = "x"+ rewards.value.ToString();
        outLine.SetActive(day <= currentStreak ? true : false);
    }

    public void SetBigRewardData(int day, List<Reward> reward)
    {
        dayTxt.text = $"Day {day}";

        for(int i= 0; i < reward.Count; i++)
        {
            rewardIcons[i].sprite =  rewardSprs[i];
            rewardsValue[i].text = "x" + reward[i].value.ToString();
        }
        
    }

}
