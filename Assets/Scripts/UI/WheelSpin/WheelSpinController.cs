using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class WheelSpinController : MonoBehaviour
{
    public static WheelSpinController instance;
    
    public List<RewardsOnWheel> listRewards = new List<RewardsOnWheel>();
    public List<Sprite> sprs = new List<Sprite>();
    public bool wheelOn;
    private float rotatePower;
    private float stopPower;
    
    Rigidbody2D rbody;
    RewardsOnWheel.RewardType rewardType;
    int inRotate;
    int reward;
    float t;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rotatePower = Random.Range(1000, 5000);
        stopPower = Random.Range(100, 500);
        LoadRewardData();
    }

    // Update is called once per frame
    void Update()
    {
        if (rbody.angularVelocity > 0)
        {
            rbody.angularVelocity -= stopPower * Time.deltaTime;
            rbody.angularVelocity = Mathf.Clamp(rbody.angularVelocity, 0, 1440);
        }
        if (rbody.angularVelocity == 0 && inRotate == 1)
        {
            t += 1 * Time.deltaTime;
            if (t >= 0.5f)
            {
                GetReward();
                inRotate = 0;
                t = 0;
                AudioManager.Instance.PlaySFX(AudioManager.Instance.soundEff[6]);
            }
        }
    }

    public void Rotete()
    {
        //AudioManager.Instance.PlaySFX("Spin");
        if (inRotate == 0)
        {
           
            rbody.AddTorque(rotatePower);
            inRotate = 1;
        }
    }
    private void LoadRewardData()
    {
        foreach (var i in listRewards)
        {
            if(i.type == RewardsOnWheel.RewardType.Coin)
            {
                i.rewardSpr = sprs[0];
            }
            else if(i.type == RewardsOnWheel.RewardType.ReverseMove)
            {
                i.rewardSpr= sprs[1];
            }
            else if(i.type == RewardsOnWheel.RewardType.UnlockHole)
            {
                i.rewardSpr = sprs[2];
            }
            else if(i.type == RewardsOnWheel.RewardType.MoreTimeTicket)
            {
                i.rewardSpr = sprs[3];
            }
            else if(i.type == RewardsOnWheel.RewardType.RePlayLevel)
            {
                i.rewardSpr = sprs[4];
            }
            i.rewardObj.GetComponent<Image>().sprite = i.rewardSpr;
            //RewardPanel.instance.RewardPopUpFromWheel(i.rewardSpr);
            i.rewardObj.GetComponent<Image>().SetNativeSize();
            i.rewardDisplay.text = "x" + i.rewardValue;
        }
    }
    
    public void GetReward()
    {
        float rot = transform.eulerAngles.z;

        if (rot > -22.5 && rot <= 22.5)
        {
            rewardType = listRewards[0].type;
            reward = listRewards[0].rewardValue;
            UpdateRewardData();
        }
        else if (rot > 22.5 && rot <= 67.5)
        {
            rewardType = listRewards[1].type;
            reward = listRewards[1].rewardValue;
            
            UpdateRewardData();
        }
        else if (rot > 67.5 && rot <= 112.5)
        {   
            rewardType = listRewards[2].type;
            reward = listRewards[2].rewardValue;
            
            UpdateRewardData();
        }
        else if (rot > 112.5 && rot <= 157.5)
        {   
            rewardType = listRewards[3].type;
            reward = listRewards[3].rewardValue;
            UpdateRewardData();
        }
        else if (rot > 157.5 && rot <= 202.5)
        {   
            rewardType = listRewards[4].type;
            reward = listRewards[4].rewardValue;
            
            UpdateRewardData();
        }
        else if (rot >202.5 && rot <= 247.5)
        {   
            rewardType = listRewards[5].type;
            reward = listRewards[5].rewardValue;
            UpdateRewardData();
        }
        else if(rot > 247.5 && rot <= 292.5)
        {
            rewardType = listRewards[6].type;
            reward = listRewards[6].rewardValue;
            UpdateRewardData();
        }
        else if(rot > 292.5 && rot <= 337.5)
        {
            rewardType = listRewards[7].type;
            reward = listRewards[7].rewardValue;
            //RewardPanel.instance.RewardPopUpFromWheel(tpye)
            UpdateRewardData();
        }
    
    }
    public void UpdateRewardData()
    {
        if (rewardType == RewardsOnWheel.RewardType.Coin)
        {
            RewardPanel.instance.RewardPopUpFromWheel(sprs[0],reward);
            UIManager.instance.coin += reward;
        }
        else if(rewardType == RewardsOnWheel.RewardType.RePlayLevel)
        {
            RewardPanel.instance.RewardPopUpFromWheel(sprs[4], reward);
            UIManager.instance.replayValue += reward;
        }
        else if(rewardType == RewardsOnWheel.RewardType.ReverseMove)
        {
            RewardPanel.instance.RewardPopUpFromWheel(sprs[1], reward);
            UIManager.instance.reverseValue += reward;
        }
        else if(rewardType == RewardsOnWheel.RewardType.UnlockHole)
        {
            RewardPanel.instance.RewardPopUpFromWheel(sprs[2], reward);
            UIManager.instance.unlockHoleValue += reward;
        }
        else if(rewardType == RewardsOnWheel.RewardType.MoreTimeTicket)
        {
            RewardPanel.instance.RewardPopUpFromWheel(sprs[3], reward);
            UIManager.instance.bonusTimeTicketValue += reward;
        }
        UIManager.instance.RewardPageOn();
        UIManager.instance.UpdatePlayerUI();
        GameManager.Instance.UpdatePlayerDataFromUI();
    }
}

[System.Serializable]
public class RewardsOnWheel
{
    public enum RewardType
    {
        Coin,
        RePlayLevel,
        ReverseMove,
        UnlockHole,
        MoreTimeTicket
    }
    public RewardType type;
    public GameObject rewardObj;
    public Sprite rewardSpr;
    public int rewardValue;
    public Text rewardDisplay;
}